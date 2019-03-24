namespace formulate.app.Controllers
{

    // Namespaces.
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Web.Http;
    using System.Xml.Linq;
    using Umbraco.Core.Logging;
    using Umbraco.Core.Packaging;
    using Umbraco.Web.Editors;
    using Umbraco.Web.Mvc;
    using Umbraco.Web.WebApi.Filters;
    using Constants = Umbraco.Core.Constants;
    using Resources = Properties.Resources;

    /// <summary>
    /// Controller that handles operations related to setup of
    /// Formulate.
    /// </summary>
    [PluginController("Formulate")]
    [UmbracoApplicationAuthorize(Constants.Applications.Settings)]
    public class SetupController : UmbracoAuthorizedJsonController
    {

        #region Properties

        /// <summary>
        /// Runs package actions.
        /// </summary>
        private IPackageActionRunner PackageActionRunner { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Primary constructor.
        /// </summary>
        /// <param name="packageActionRunner">
        /// The service to run package actions.
        /// </param>
        public SetupController(IPackageActionRunner packageActionRunner)
        {
            PackageActionRunner = packageActionRunner;
        }

        #endregion

        #region Web Methods

        /// <summary>
        /// Gives the current user permission to access the
        /// "Formulate" section in Umbraco.
        /// </summary>
        /// <returns>
        /// An object indicating whether or not the operation
        /// was sucessful.
        /// </returns>
        [HttpPost]
        public object PermitAccessToFormulate()
        {
            try
            {

                // Variables.
                var actionXml = Resources.GrantPermissionToSection;
                var doc = default(XElement);
                using (var reader = new StringReader(actionXml))
                {
                    doc = XElement.Load(actionXml);
                }

                // Grant access permission.
                var errors = default(IEnumerable<string>);
                PackageActionRunner.RunPackageAction("Formulate",
                    "Formulate.GrantPermissionToSection", doc, out errors);

                // Indicate success.
                return new
                {
                    Success = true
                };

            }
            catch (Exception ex)
            {

                // Error (log and indicate failure).
                var message = "An error occurred while attempting to set permissions.";
                Logger.Error<SetupController>(ex, message);
                return new
                {
                    Success = false,
                    Reason = message
                };

            }
        }

        #endregion

    }

}