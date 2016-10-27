﻿namespace formulate.api.Controllers
{

    // Namespaces.
    using app.Managers;
    using app.Resolvers;
    using core.Types;
    using core.Utilities;
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

        #region Properties

        /// <summary>
        /// Configuration manager.
        /// </summary>
        private IConfigurationManager Config
        {
            get
            {
                return Configuration.Current.Manager;
            }
        }

        #endregion


        #region Action Methods

        /// <summary>
        /// Handles form submissions.
        /// </summary>
        /// <returns>
        /// A JSON object indicating success or failure.
        /// </returns>
        [HttpPost()]
        public ActionResult Submit()
        {

            // Variables.
            var tempGuid = default(Guid);
            var keys = Request.Form.AllKeys;
            var fileKeys = Request.Files.AllKeys;
            var formId = Guid.Parse(Request["FormId"]);
            var pageId = NumberUtility.AttemptParseInt(Request["PageId"]);
            var pageNode = pageId.HasValue
                ? Umbraco.TypedContent(pageId.Value)
                : null;
            var pageUrl = pageNode?.Url;
            var pageName = pageNode?.Name;


            // Get values.
            var values = keys
                .Where(x => Guid.TryParse(x, out tempGuid))
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
                .Where(x => Guid.TryParse(x, out tempGuid))
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
            var result = Submissions.SubmitForm(formId, values, fileValues, payload, options, context);


            // Return result.
            return Json(new
            {
                Success = result.Success
            });

        }

        #endregion

    }

}