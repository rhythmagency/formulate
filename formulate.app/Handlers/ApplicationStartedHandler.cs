namespace formulate.app.Handlers
{

    // Namespaces.
    using System.Configuration;
    using System.Web.Configuration;
    using System.Xml;
    using umbraco.cms.businesslogic.packager;
    using Umbraco.Core;
    using Resources = formulate.app.Properties.Resources;


    /// <summary>
    /// Handles the application started event.
    /// </summary>
    internal class ApplicationStartedHandler : ApplicationEventHandler
    {

        #region Methods

        /// <summary>
        /// Application started.
        /// </summary>
        protected override void ApplicationStarted(UmbracoApplicationBase umbracoApplication,
            ApplicationContext applicationContext)
        {
            var isInstalled = GetInstalledVersion().HasValue;
            if (!isInstalled)
            {
                AddSection(applicationContext);
                AddDashboard();
                AddVersion();
            }
        }


        /// <summary>
        /// Gets the installed version number.
        /// </summary>
        /// <returns>The installed version number, or null.</returns>
        private double? GetInstalledVersion()
        {
            var version = ConfigurationManager.AppSettings["Formulate:Version"];
            if (string.IsNullOrWhiteSpace(version))
            {
                return null;
            }
            else
            {
                var numVersion = default(double);
                if (double.TryParse(version, out numVersion))
                {
                    return numVersion;
                }
                else
                {
                    return null;
                }
            }
        }


        /// <summary>
        /// Adds the Formulate section to Umbraco.
        /// </summary>
        /// <param name="applicationContext">
        /// The current application context.
        /// </param>
        private void AddSection(ApplicationContext applicationContext)
        {
            var service = applicationContext.Services.SectionService;
            var existingSection = service.GetByAlias("formulate");
            if (existingSection == null)
            {
                service.MakeNew("Formulate", "formulate", "icon-formulate-clipboard", 6);
            }
        }


        /// <summary>
        /// Adds the Formulate dashboard to the Formulate section.
        /// </summary>
        private void AddDashboard()
        {
            //TODO: Before adding the dashboard, detect if it exists.
            var doc = new XmlDocument();
            var actionXml = Resources.AddDashboard;
            doc.LoadXml(actionXml);
            PackageAction.RunPackageAction("Formulate", "addDashboardSection", doc.FirstChild);
        }


        /// <summary>
        /// Adds the Formulate version number to the web.config.
        /// </summary>
        private void AddVersion()
        {
            var config = WebConfigurationManager.OpenWebConfiguration("~");
            var settings = config.AppSettings.Settings;
            //TODO: Load version number from some central location.
            settings.Add("Formulate:Version", "0.0");
            config.Save();
        }

        #endregion

    }

}