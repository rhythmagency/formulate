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
    using Umbraco.Core.IO;
    using Umbraco.Core.Logging;
    using Constants = formulate.meta.Constants;
    using Resources = formulate.app.Properties.Resources;
    using SettingConstants = formulate.core.Constants.Settings;


    /// <summary>
    /// Handles the application started event.
    /// </summary>
    internal class ApplicationStartedHandler : ApplicationEventHandler
    {

        #region Constants

        private const string DeveloperSectionXPath = @"/dashBoard/section[@alias='StartupDeveloperDashboardSection']";
        private const string MissingDeveloperSection = @"Unable to location StartupDeveloperDashboardSection in the dashboard.config. The Formulate tab will not be added to the Developer section.";

        #endregion


        #region Methods

        /// <summary>
        /// Application started.
        /// </summary>
        protected override void ApplicationStarted(
            UmbracoApplicationBase umbracoApplication,
            ApplicationContext applicationContext)
        {
            var version = GetInstalledVersion();
            var isInstalled = version != null;
            var needsUpgrade = !Constants.Version.InvariantEquals(version);
            if (!isInstalled)
            {
                AddSection(applicationContext);
                AddDashboard();
                AddFormulateDeveloperTab();
                PermitAccess();
                AddVersion();
            }
            else if (needsUpgrade)
            {
                AddVersion();
            }
        }


        /// <summary>
        /// Gets the installed version.
        /// </summary>
        /// <returns>The installed version, or null.</returns>
        private string GetInstalledVersion()
        {
            var key = SettingConstants.VersionKey;
            var version = ConfigurationManager.AppSettings[key];
            if (string.IsNullOrWhiteSpace(version))
            {
                version = null;
            }
            return version;
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
                service.MakeNew("Formulate", "formulate",
                    "icon-formulate-clipboard", 6);
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
                PackageAction.RunPackageAction("Formulate",
                    "addDashboardSection", doc.FirstChild);
            }
        }


        /// <summary>
        /// Indicates whether or not the "FormulateSection" exists in
        /// the dashboard.config.
        /// </summary>
        private bool DashboardExists()
        {
            var config = WebConfigurationManager.OpenWebConfiguration("~");
            var dashboard = config.GetSection("umbracoConfiguration/dashBoard")
                as IDashboardSection;
            var hasFormulate = dashboard.Sections.Any(x =>
                "FormulateSection".InvariantEquals(x.Alias));
            return hasFormulate;
        }


        /// <summary>
        /// Indicates whether or not the "Formulate" installation tab
        /// exists in the developer section of the dashboard.config.
        /// </summary>
        /// <returns>
        /// True, if the tab exists; otherwise, false.
        /// </returns>
        private bool FormulateDeveloperTabExists()
        {
            var config = WebConfigurationManager.OpenWebConfiguration("~");
            var dashboard = config.GetSection("umbracoConfiguration/dashBoard")
                as IDashboardSection;
            var developerSection = dashboard.Sections
                .FirstOrDefault(x => "StartupDeveloperDashboardSection"
                    .InvariantEquals(x.Alias));
            var tab = developerSection == null
                ? null
                : developerSection.Tabs.FirstOrDefault(x =>
                    "Formulate".InvariantEquals(x.Caption));
            var hasTab = tab != null;
            return hasTab;
        }


        /// <summary>
        /// Adds the "Formulate" tab to the developer section of the
        /// dashboard.config.
        /// </summary>
        private void AddFormulateDeveloperTab()
        {
            var exists = FormulateDeveloperTabExists();
            if (!exists)
            {

                // Get developer section from Dashboard.config.
                var dashboardPath = SystemFiles.DashboardConfig;
                var dashboardDoc = XmlHelper.OpenAsXmlDocument(dashboardPath);
                var devSection = dashboardDoc.SelectSingleNode(
                    DeveloperSectionXPath);


                // If the developer section isn't found, log a warning and
                // skip adding tab.
                if (devSection == null)
                {
                    LogHelper.Warn<ApplicationStartedHandler>(
                        MissingDeveloperSection);
                    return;
                }


                // Load tab into developer section.
                var tempDoc = new XmlDocument();
                var newNode = XmlHelper.ImportXmlNodeFromText(
                    Resources.FormulateTab, ref tempDoc);
                newNode = dashboardDoc.ImportNode(newNode, true);
                devSection.AppendChild(newNode);


                // Save new Dashboard.config.
                var mappedDashboardPath = IOHelper.MapPath(dashboardPath);
                dashboardDoc.Save(mappedDashboardPath);

            }
        }


        /// <summary>
        /// Adds the Formulate version number to the web.config.
        /// </summary>
        private void AddVersion()
        {

            // Variables.
            var key = SettingConstants.VersionKey;
            var config = WebConfigurationManager.OpenWebConfiguration("~");
            var settings = config.AppSettings.Settings;


            // Remove existing version setting.
            if (settings.AllKeys.Any(x => key.InvariantEquals(x)))
            {
                settings.Remove(key);
            }


            // Add version setting.
            settings.Add(key, Constants.Version);
            config.Save();

        }


        /// <summary>
        /// Permits all users to access Formulate if configured in the web.config.
        /// </summary>
        private void PermitAccess()
        {

            // Variables.
            var key = SettingConstants.EnsureUsersCanAccess;
            var ensure = ConfigurationManager.AppSettings[key];


            // Should all users be given access to Formulate?
            if (string.IsNullOrWhiteSpace(ensure))
            {
                return;
            }


            // Variables.
            var doc = new XmlDocument();
            var actionXml = Resources.GrantAllUsersPermissionToSection;
            doc.LoadXml(actionXml);


            // Grant access permission.
            PackageAction.RunPackageAction("Formulate",
                "GrantPermissionToSection", doc.FirstChild);

        }

        #endregion

    }

}