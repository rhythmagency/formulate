namespace formulate.app.Controllers
{

    // Namespaces.
    using core.Constants;
    using System.Configuration;
    using System.Linq;
    using System.Web.Http;
    using Umbraco.Web.Mvc;
    using Umbraco.Web.WebApi;
    using Umbraco.Web.WebApi.Filters;


    /// <summary>
    /// Controller for working with server configuration.
    /// </summary>
    [PluginController("formulate")]
    [UmbracoApplicationAuthorize("formulate")]
    public class ServerConfigurationController : UmbracoAuthorizedApiController
    {

        #region Web Methods

        /// <summary>
        /// Returns a value indicating whether or not the server has been configured for Recaptcha.
        /// </summary>
        /// <returns>
        /// True, if the server has been configured for Recaptcha; otherwise, false.
        /// </returns>
        [HttpGet]
        public object HasConfiguredRecaptcha()
        {
            var values = new[]
            {
                ConfigurationManager.AppSettings[Settings.RecaptchaSecretKey],
                ConfigurationManager.AppSettings[Settings.RecaptchaSiteKey]
            };
            var allExist = values.All(x => !string.IsNullOrWhiteSpace(x));
            return new
            {
                Success = true,
                HasConfigured = allExist
            };
        }

        /// <summary>
        /// Returns a value indicating whether or not the server has been configured for logging form JSON output.
        /// </summary>
        /// <returns>
        /// True, if the server has been configured for logging JSON; otherwise, false.
        /// </returns>
        [HttpGet]
        public object HasEnableJSONFormLogging()
        {
            var values = new[]
            {
                ConfigurationManager.AppSettings[Settings.EnableJSONFormLogging],
            };
            var allExist = values.All(x => !string.IsNullOrWhiteSpace(x));
            return new
            {
                Success = true,
                HasConfigured = allExist
            };
        }

        #endregion

    }

}