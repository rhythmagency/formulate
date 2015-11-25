namespace formulate.app.Handlers
{

    // Namespaces.
    using System.Configuration;
    using System.Linq;
    using System.Web.Configuration;
    using System.Xml;
    using umbraco.cms.businesslogic.packager;
    using Umbraco.Core;
    using Umbraco.Core.Configuration.Dashboard;
    using Resources = formulate.app.Properties.Resources;
    using Trees;


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
            //TODO: Remove.
            AddTree(applicationContext);
            var isInstalled = GetInstalledVersion().HasValue;
            if (!isInstalled)
            {
                AddSection(applicationContext);
                AddTree(applicationContext);
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


        private void AddTree(ApplicationContext applicationContext)
        {
            var service = applicationContext.Services.ApplicationTreeService;
            var existingTree = service.GetByAlias("formulate");
            if (existingTree == null)
            {
                // Initializing a tree opens it when you navigate to that section.
                var shouldInitialize = true;
                var sortOrder = (byte)0;
                var applicationAlias = "formulate";
                var alias = "dataSources";
                var title = "Data Sources";
                var closedIcon = "icon-folder-open";
                var openIcon = "icon-folder";
                var type = typeof(DataSourcesTreeController).GetFullNameWithAssembly();
                service.MakeNew(shouldInitialize, sortOrder, applicationAlias, alias, title, closedIcon,
                    openIcon, type);
            }
        }


        /// <summary>
        /// Adds the Formulate dashboard to the Formulate section.
        /// </summary>
        private void AddDashboard()
        {
            var exists = DashboardExists();
            if (!exists)
            {
                var doc = new XmlDocument();
                var actionXml = Resources.AddDashboard;
                doc.LoadXml(actionXml);
                PackageAction.RunPackageAction("Formulate", "addDashboardSection", doc.FirstChild);
            }
        }


        /// <summary>
        /// Indicates whether or not the "FormulateSection" exists in the dashboard.config.
        /// </summary>
        private bool DashboardExists()
        {
            var config = WebConfigurationManager.OpenWebConfiguration("~");
            var dashboard = config.GetSection("umbracoConfiguration/dashBoard") as IDashboardSection;
            var hasFormulate = dashboard.Sections.Any(x => "FormulateSection".InvariantEquals(x.Alias));
            return hasFormulate;
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