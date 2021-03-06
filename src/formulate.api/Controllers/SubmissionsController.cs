﻿namespace formulate.api.Controllers
{

    // Namespaces.
    using app.Managers;
    using app.Persistence;
    using core.Types;
    using System;
    using System.IO;
    using System.Linq;
    using System.Web.Mvc;
    using Umbraco.Web.Mvc;


    /// <summary>
    /// Controller for form submissions.
    /// </summary>
    [PluginController("formulate")]
    public class SubmissionsController : SurfaceController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SubmissionsController"/> class.
        /// </summary>
        /// <param name="configurationManager">
        /// The configuration manager.
        /// </param>
        /// <param name="formPersistence">
        /// The form persistence.
        /// </param>
        /// <param name="validationPersistence">
        /// The validation persistence.
        /// </param>
        public SubmissionsController(IConfigurationManager configurationManager, IFormPersistence formPersistence, IValidationPersistence validationPersistence)
        {
            Config = configurationManager;
            Forms = formPersistence;
            Validations = validationPersistence;
        }

        #region Properties

        /// <summary>
        /// Gets or sets the form persistence.
        /// </summary>
        private IFormPersistence Forms { get; set; }

        /// <summary>
        /// Gets or sets the validation persistence.
        /// </summary>
        private IValidationPersistence Validations { get; set; }

        /// <summary>
        /// Gets or sets the configuration manager.
        /// </summary>
        private IConfigurationManager Config { get; set; }

        #endregion


        #region Action Methods

        /// <summary>
        /// Handles form submissions.
        /// </summary>
        /// <param name="formId">
        /// The form Id.
        /// </param>
        /// <param name="pageId">
        /// The page Id.
        /// </param>
        /// <returns>
        /// A JSON object indicating success or failure.
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Submit(Guid formId, int pageId)
        {

            // Variables.
            var keys = Request.Form.AllKeys;
            var fileKeys = Request.Files.AllKeys;
            var pageNode = Umbraco.Content(pageId);
            var pageUrl = pageNode?.Url;
            var pageName = pageNode?.Name;


            // Get values.
            var values = keys
                .Where(x => Guid.TryParse(x, out var tempGuid))
                .Select(x =>
                {
                    var fieldValue = Request.Form.GetValues(x);
                    return new FieldSubmission
                    {
                        FieldId = Guid.Parse(x),
                        FieldValues = fieldValue
                    };
                })
                .ToList();

            // Get file values.
            var fileValues = fileKeys
                .Where(x => Guid.TryParse(x, out var tempGuid))
                .Select(x =>
                {

                    // Variables.
                    var fileValue = Request.Files.Get(x);


                    // Extract file data: http://stackoverflow.com/a/16030326/2052963
                    var reader = new BinaryReader(fileValue.InputStream);
                    var fileData = reader.ReadBytes((int)fileValue.InputStream.Length);
                    var filename = fileValue.FileName;


                    // Return file field submission.
                    return new FileFieldSubmission()
                    {
                        FieldId = Guid.Parse(x),
                        FileData = fileData,
                        FileName = filename
                    };

                })
                .ToList();


            // Payload.
            var payload = new[]
            {
                new PayloadSubmission()
                {
                    Name = "URL",
                    Value = pageUrl
                },
                new PayloadSubmission()
                {
                    Name = "Page Name",
                    Value = pageName
                }
            }.Where(x => !string.IsNullOrWhiteSpace(x.Value));


            // Submit form.
            var context = new FormRequestContext()
            {
                CurrentPage = pageNode,
                HttpContext = HttpContext,
                Services = Services,
                UmbracoContext = UmbracoContext,
                UmbracoHelper = Umbraco
            };
            var options = new SubmissionOptions()
            {
                Validate = Config.EnableServerSideValidation
            };
            var result = new Submissions(Forms, Validations, Logger)
                .SubmitForm(formId, values, fileValues, payload, options, context);

            // Return result.
            return Json(new
            {
                Success = result.Success,
                ValidationErrors = result.ValidationErrors
            });
        }

        #endregion

    }
}
