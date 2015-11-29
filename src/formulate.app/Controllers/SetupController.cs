namespace formulate.app.Controllers
{

    // Namespaces.
    using System;
    using System.Web.Http;
    using Umbraco.Core;
    using Umbraco.Core.Logging;
    using Umbraco.Web;
    using Umbraco.Web.Editors;
    using Umbraco.Web.Mvc;
    using Umbraco.Web.WebApi.Filters;
    using Constants = Umbraco.Core.Constants;


    /// <summary>
    /// Controller that handles operations related to setup of
    /// Forulate.
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
                var services = ApplicationContext.Current.Services;
                var service = services.UserService;
                var security = UmbracoContext.Current.Security;
                var currentUser = security.CurrentUser;
                currentUser.AddAllowedSection("formulate");
                service.Save(currentUser, true);


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