namespace formulate.app.Handlers
{

    // Namespaces.
    using Controllers;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using System.Web;
    using System.Web.Configuration;
    using System.Web.Mvc;
    using System.Web.Routing;
    using System.Xml;
    using umbraco.cms.businesslogic.packager;
    using Umbraco.Core;
    using Umbraco.Core.Configuration.Dashboard;
    using Umbraco.Core.IO;
    using Umbraco.Core.Logging;
    using Umbraco.Web;
    using Umbraco.Web.UI.JavaScript;
    using Constants = formulate.meta.Constants;
    using DataValueConstants = formulate.app.Constants.Trees.DataValues;
    using Resources = formulate.app.Properties.Resources;
    using SettingConstants = formulate.core.Constants.Settings;
    using ValidationConstants = formulate.app.Constants.Trees.Validations;


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
            HandleInstallAndUpgrade(applicationContext);
            ServerVariablesParser.Parsing += AddServerVariables;
        }


        /// <summary>
        /// Adds server variables for use by the JavaScript.
        /// </summary>
        private void AddServerVariables(object sender,
            Dictionary<string, object> e)
        {

            // Variables.
            var httpContext = new HttpContextWrapper(HttpContext.Current);
            var routeData = new RouteData();
            var requestContext = new RequestContext(httpContext, routeData);
            var helper = new UrlHelper(requestContext);
            var key = Constants.PackageNameCamelCase;


            // Add server variables.
            e.Add(key, new Dictionary<string, string>()
            {
                { "DeleteForm",
                    helper.GetUmbracoApiService<FormsController>(x =>
                        x.DeleteForm(null)) },
                { "PersistForm",
                    helper.GetUmbracoApiService<FormsController>(x =>
                        x.PersistForm(null)) },
                { "GetFormInfo",
                    helper.GetUmbracoApiService<FormsController>(x =>
                        x.GetFormInfo(null)) },
                { "DeleteLayout",
                    helper.GetUmbracoApiService<LayoutsController>(x =>
                        x.DeleteLayout(null)) },
                { "PersistLayout",
                    helper.GetUmbracoApiService<LayoutsController>(x =>
                        x.PersistLayout(null)) },
                { "GetLayoutInfo",
                    helper.GetUmbracoApiService<LayoutsController>(x =>
                        x.GetLayoutInfo(null)) },
                { "DeleteValidation",
                    helper.GetUmbracoApiService<ValidationsController>(x =>
                        x.DeleteValidation(null)) },
                { "PersistValidation",
                    helper.GetUmbracoApiService<ValidationsController>(x =>
                        x.PersistValidation(null)) },
                { "GetValidationInfo",
                    helper.GetUmbracoApiService<ValidationsController>(x =>
                        x.GetValidationInfo(null)) },
                { "GetValidationsInfo",
                    helper.GetUmbracoApiService<ValidationsController>(x =>
                        x.GetValidationsInfo(null)) },
                { "DeleteDataValue",
                    helper.GetUmbracoApiService<DataValuesController>(x =>
                        x.DeleteDataValue(null)) },
                { "PersistDataValue",
                    helper.GetUmbracoApiService<DataValuesController>(x =>
                        x.PersistDataValue(null)) },
                { "GetDataValueInfo",
                    helper.GetUmbracoApiService<DataValuesController>(x =>
                        x.GetDataValueInfo(null)) },
                { "GetDataValuesInfo",
                    helper.GetUmbracoApiService<DataValuesController>(x =>
                        x.GetDataValuesInfo(null)) },
                { "PersistFolder",
                    helper.GetUmbracoApiService<FoldersController>(x =>
                        x.PersistFolder(null)) },
                { "GetFieldTypes",
                    helper.GetUmbracoApiService<FieldsController>(x =>
                        x.GetFieldTypes()) },
                { "PermitAccess",
                    helper.GetUmbracoApiService<SetupController>(x =>
                        x.PermitAccessToFormulate()) },
                { "GetEntityChildren",
                    helper.GetUmbracoApiService<EntitiesController>(x =>
                        x.GetEntityChildren(null)) },
                { "GetEntity",
                    helper.GetUmbracoApiService<EntitiesController>(x =>
                        x.GetEntity(null)) },
                { "EditLayoutBase", "/formulate/formulate/editLayout/" },
                { "EditValidationBase",
                    "/formulate/formulate/editValidation/" },
                { "EditDataValueBase",
                    "/formulate/formulate/editDataValue/" },
                { "Validation.RootId", ValidationConstants.Id },
                { "DataValue.RootId", DataValueConstants.Id }
            });

        }


        /// <summary>
        /// Handles install and upgrade operations.
        /// </summary>
        private void HandleInstallAndUpgrade(
            ApplicationContext applicationContext)
        {
            var version = GetInstalledVersion();
            var isInstalled = version != null;
            var needsUpgrade = !Constants.Version.InvariantEquals(version);
            if (!isInstalled)
            {
                HandleInstall(applicationContext);
            }
            else if (needsUpgrade)
            {
                EnsureVersion();
            }
        }


        /// <summary>
        /// Handles install operations.
        /// </summary>
        private void HandleInstall(ApplicationContext applicationContext)
        {
            AddSection(applicationContext);
            AddDashboard();
            AddFormulateDeveloperTab();
            PermitAccess();
            AddConfigurationGroup();
            EnsureVersion();
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
        /// Adds or replaces the Formulate version number in the web.config.
        /// </summary>
        private void EnsureVersion()
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
                "Formulate.GrantPermissionToSection", doc.FirstChild);

        }


        /// <summary>
        /// Transforms the web.config to add the Formulate
        /// configuration group.
        /// </summary>
        private void AddConfigurationGroup()
        {

            // Does the section group already exist?
            var config = WebConfigurationManager.OpenWebConfiguration("~");
            var groupName = "formulateConfiguration";
            var group = config.GetSectionGroup(groupName);
            var exists = group != null;


            // Only add the group if it doesn't exist.
            if (!exists)
            {

                // Variables.
                var doc = new XmlDocument();
                var actionXml = Resources.TransformWebConfig;
                doc.LoadXml(actionXml);


                // Grant access permission.
                PackageAction.RunPackageAction("Formulate",
                    "Formulate.TransformXmlFile", doc.FirstChild);

            }

        }

        #endregion

    }

}