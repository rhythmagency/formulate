namespace formulate.app.Controllers
{

    // Namespaces.
    using Helpers;
    using Models.Requests;
    using Persistence;
    using System;
    using System.Linq;
    using System.Web.Http;
    using Umbraco.Core;
    using Umbraco.Core.Logging;
    using Umbraco.Web;
    using Umbraco.Web.Editors;
    using Umbraco.Web.Mvc;
    using Umbraco.Web.WebApi.Filters;
    using CoreConstants = Umbraco.Core.Constants;


    /// <summary>
    /// Controller for Formulate configured forms. This variation can be used in the content
    /// section (e.g., for the form picker).
    /// </summary>
    [PluginController("formulate")]
    [UmbracoApplicationAuthorize("formulate", CoreConstants.Applications.Content)]
    public class ConfiguredFormsContentController : UmbracoAuthorizedJsonController
    {

        #region Constants

        private const string UnhandledError = @"An unhandled error occurred. Refer to the error log.";
        private const string GetConFormInfoError = @"An error occurred while attempting to get the configured form info for a Formulate configured form.";
        private const string FormNotFoundError = @"The configured Formulate form requested could not be found.";

        #endregion


        #region Properties

        private IConfiguredFormPersistence Persistence { get; set; }

        #endregion


        #region Constructors
        /// <summary>
        /// Primary constructor.
        /// </summary>
        /// <param name="context">Umbraco context.</param>
        public ConfiguredFormsContentController(IConfiguredFormPersistence configuredFormPersistence)
        {
            Persistence = configuredFormPersistence;
        }

        #endregion


        #region Web Methods

        /// <summary>
        /// Returns info about the configured form with the specified ID.
        /// </summary>
        /// <param name="request">
        /// The request to get the configured form info.
        /// </param>
        /// <returns>
        /// An object indicating success or failure, along with some
        /// accompanying data.
        /// </returns>
        [HttpGet]
        public object GetConfiguredFormInfo([FromUri] GetConfiguredFormInfoRequest request)
        {

            // Variables.
            var result = default(object);
            var rootId = CoreConstants.System.Root.ToInvariantString();


            // Catch all errors.
            try
            {

                // Variables.
                var id = GuidHelper.GetGuid(request.ConFormId);
                var configuredForm = Persistence.Retrieve(id);


                // Check for a null configured form.
                if (configuredForm == null)
                {
                    result = new
                    {
                        Success = false,
                        Reason = FormNotFoundError
                    };
                    return result;
                }


                // Variables.
                var partialPath = configuredForm.Path
                    .Select(x => GuidHelper.GetString(x));
                var fullPath = new[] { rootId }
                    .Concat(partialPath)
                    .ToArray();


                // Set result.
                result = new
                {
                    Success = true,
                    ConFormId = GuidHelper.GetString(configuredForm.Id),
                    Path = fullPath,
                    Name = configuredForm.Name,
                    LayoutId = configuredForm.LayoutId.HasValue
                        ? GuidHelper.GetString(configuredForm.LayoutId.Value)
                        : null,
                    TemplateId = configuredForm.TemplateId.HasValue
                        ? GuidHelper.GetString(configuredForm.TemplateId.Value)
                        : null
                };

            }
            catch (Exception ex)
            {

                // Error.
                Logger.Error<ConfiguredFormsController>(ex, GetConFormInfoError);
                result = new
                {
                    Success = false,
                    Reason = UnhandledError
                };

            }


            // Return result.
            return result;

        }

        #endregion

    }

}