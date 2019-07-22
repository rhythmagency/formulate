namespace formulate.app.Controllers
{

    // Namespaces.
    using Helpers;
    using Managers;
    using System;
    using System.Linq;
    using System.Web.Http;
    using Umbraco.Core.Logging;
    using Umbraco.Web;
    using Umbraco.Web.Editors;
    using Umbraco.Web.Mvc;
    using Umbraco.Web.WebApi.Filters;
    using CoreConstants = Umbraco.Core.Constants;


    /// <summary>
    /// Controller for Formulate templates.
    /// </summary>
    [PluginController("formulate")]
    [UmbracoApplicationAuthorize("formulate")]
    public class TemplatesController : UmbracoAuthorizedJsonController
    {

        #region Constants

        private const string UnhandledError = @"An unhandled error occurred. Refer to the error log.";
        private const string GetTemplatesError = @"An error occurred while attempting to get the templates.";

        #endregion


        #region Properties

        /// <summary>
        /// Configuration manager.
        /// </summary>
        private IConfigurationManager Config { get; set; }

        #endregion


        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public TemplatesController(IConfigurationManager configurationManager)
        {
            Config = configurationManager;
        }

        #endregion


        #region Web Methods

        /// <summary>
        /// Returns the templates.
        /// </summary>
        /// <returns>
        /// An object indicating success or failure, along with
        /// information about templates.
        /// </returns>
        [HttpGet]
        public object GetTemplates()
        {

            // Variables.
            var result = default(object);


            // Catch all errors.
            try
            {

                // Variables.
                var templates = Config.Templates;


                // Return results.
                result = new
                {
                    Success = true,
                    Templates = templates.Select(x => new
                    {
                        Id = GuidHelper.GetString(x.Id),
                        Name = x.Name
                    }).ToArray()
                };

            }
            catch (Exception ex)
            {

                // Error.
                Logger.Error<TemplatesController>(ex, GetTemplatesError);
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