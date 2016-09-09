namespace formulate.app.Controllers
{

    // Namespaces.
    using System;
    using System.Web.Http;
    using System.Xml;
    using umbraco.cms.businesslogic.packager;
    using Umbraco.Core.Logging;
    using Umbraco.Web;
    using Umbraco.Web.Editors;
    using Umbraco.Web.Mvc;
    using Umbraco.Web.WebApi.Filters;
    using Constants = Umbraco.Core.Constants;
    using Resources = formulate.app.Properties.Resources;


    /// <summary>
    /// Controller that handles operations related to setup of
    /// Formulate.
    /// </summary>
    [PluginController("Formulate")]
    [UmbracoApplicationAuthorize(Constants.Applications.Developer)]
    public class SetupController : UmbracoAuthorizedJsonController
    {

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public SetupController()
            : this(UmbracoContext.Current)
        {
        }


        /// <summary>
        /// Primary constructor.
        /// </summary>
        public SetupController(
            UmbracoContext umbracoContext)
            : base(umbracoContext)
        {
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
                var doc = new XmlDocument();
                var actionXml = Resources.GrantPermissionToSection;
                doc.LoadXml(actionXml);


                // Grant access permission.
                PackageAction.RunPackageAction("Formulate",
                    "Formulate.GrantPermissionToSection", doc.FirstChild);


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
                LogHelper.Error<SetupController>(message, ex);
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