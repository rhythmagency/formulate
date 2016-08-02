namespace formulate.api.Controllers
{

    // Namespaces.
    using app.Managers;
    using app.Resolvers;
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


            // Submit form.
            var result = Submissions.SubmitForm(formId, values, fileValues, new SubmissionOptions()
            {
                Validate = Config.EnableServerSideValidation
            });


            // Return result.
            return Json(new
            {
                Success = result.Success
            });

        }

        #endregion

    }

}