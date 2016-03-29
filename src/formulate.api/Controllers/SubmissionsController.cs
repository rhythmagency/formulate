namespace formulate.api.Controllers
{

    // Namespaces.
    using app.Managers;
    using app.Resolvers;
    using core.Types;
    using System;
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
            var formId = Guid.Parse(Request["FormId"]);


            // Get values.
            var values = keys
                .Where(x => Guid.TryParse(x, out tempGuid))
                .Select(x => new FieldSubmission
                {
                    FieldId = Guid.Parse(x),
                    FieldValues = Request.Form.GetValues(x)
                })
                .ToList();


            // Submit form.
            var result = Submissions.SubmitForm(formId, values, new SubmissionOptions()
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