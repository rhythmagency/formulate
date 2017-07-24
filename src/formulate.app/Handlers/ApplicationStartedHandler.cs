namespace formulate.app.Handlers
{

    // Namespaces.
    using Controllers;
    using core.Extensions;
    using Persistence.Internal.Sql.Models;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using System.Timers;
    using System.Web;
    using System.Web.Configuration;
    using System.Web.Hosting;
    using System.Web.Mvc;
    using System.Web.Routing;
    using System.Xml;
    using umbraco.cms.businesslogic.packager;
    using Umbraco.Core;
    using Umbraco.Core.Configuration.Dashboard;
    using Umbraco.Core.IO;
    using Umbraco.Core.Logging;
    using Umbraco.Core.Persistence;
    using Umbraco.Web;
    using Umbraco.Web.UI.JavaScript;
    using DataValueConstants = Constants.Trees.DataValues;
    using FormConstants = Constants.Trees.Forms;
    using LayoutConstants = Constants.Trees.Layouts;
    using MetaConstants = meta.Constants;
    using Resources = Properties.Resources;
    using SettingConstants = core.Constants.Settings;
    using ValidationConstants = Constants.Trees.Validations;


    /// <summary>
    /// Handles the application started event.
    /// </summary>
    internal class ApplicationStartedHandler : ApplicationEventHandler
    {

        #region Constants

        private const string DeveloperSectionXPath = @"/dashBoard/section[@alias='StartupDeveloperDashboardSection']";
        private const string MissingDeveloperSection = @"Unable to locate StartupDeveloperDashboardSection in the dashboard.config. The Formulate tab will not be added to the Developer section.";
        private const string InstallActionsError = @"An unknown error occurred while attempting to asynchronously run the install actions for Formulate.";
        private const string TableCreationError = @"An error occurred while attempting to create the FormulateSubmissions table.";

        #endregion


        #region Properties

        private static List<Action> InstallActions { get; set; }
        private static object InstallActionsLock { get; set; }
        private static Timer InstallTimer { get; set; }

        #endregion


        #region Constructors

        /// <summary>
        /// Static constructor.
        /// </summary>
        static ApplicationStartedHandler()
        {
            InstallActions = new List<Action>();
            InstallActionsLock = new object();
            InstallTimer = null;
        }

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
            InitializeDatabase(applicationContext);
            ServerVariablesParser.Parsing += AddServerVariables;
        }


        /// <summary>
        /// Modifies the database (e.g., adding necessary tables).
        /// </summary>
        /// <param name="applicationContext">
        /// The application context.
        /// </param>
        private void InitializeDatabase(ApplicationContext applicationContext)
        {
            var dbContext = applicationContext.DatabaseContext;
            var logger = applicationContext.ProfilingLogger.Logger;
            var db = dbContext.Database;
            var dbHelper = new DatabaseSchemaHelper(db, logger, dbContext.SqlSyntax);
            try
            {
                if (!dbHelper.TableExist("FormulateSubmission"))
                {
                    dbHelper.CreateTable<FormulateSubmission>();
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error<ApplicationStartedHandler>(TableCreationError, ex);
            }
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
            var key = MetaConstants.PackageNameCamelCase;


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
                { "MoveForm",
                    helper.GetUmbracoApiService<FormsController>(x =>
                        x.MoveForm(null)) },
                { "DeleteConfiguredForm",
                    helper.GetUmbracoApiService<ConfiguredFormsController>(x =>
                        x.DeleteConfiguredForm(null)) },
                { "PersistConfiguredForm",
                    helper.GetUmbracoApiService<ConfiguredFormsController>(x =>
                        x.PersistConfiguredForm(null)) },
                { "GetConfiguredFormInfo",
                    helper.GetUmbracoApiService<ConfiguredFormsContentController>(x =>
                        x.GetConfiguredFormInfo(null)) },
                { "DeleteLayout",
                    helper.GetUmbracoApiService<LayoutsController>(x =>
                        x.DeleteLayout(null)) },
                { "PersistLayout",
                    helper.GetUmbracoApiService<LayoutsController>(x =>
                        x.PersistLayout(null)) },
                { "GetLayoutInfo",
                    helper.GetUmbracoApiService<LayoutsController>(x =>
                        x.GetLayoutInfo(null)) },
                { "GetLayoutKinds",
                    helper.GetUmbracoApiService<LayoutsController>(x =>
                        x.GetLayoutKinds()) },
                { "MoveLayout",
                    helper.GetUmbracoApiService<LayoutsController>(x =>
                        x.MoveLayout(null)) },
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
                { "GetValidationKinds",
                    helper.GetUmbracoApiService<ValidationsController>(x =>
                        x.GetValidationKinds()) },
                { "MoveValidation",
                    helper.GetUmbracoApiService<ValidationsController>(x =>
                        x.MoveValidation(null)) },
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
                { "GetDataValueKinds",
                    helper.GetUmbracoApiService<DataValuesController>(x =>
                        x.GetDataValueKinds()) },
                { "GetDataValueSuppliers",
                    helper.GetUmbracoApiService<DataValuesController>(x =>
                        x.GetDataValueSuppliers()) },
                { "MoveDataValue",
                    helper.GetUmbracoApiService<DataValuesController>(x =>
                        x.MoveDataValue(null)) },
                { "PersistFolder",
                    helper.GetUmbracoApiService<FoldersController>(x =>
                        x.PersistFolder(null)) },
                { "GetFolderInfo",
                    helper.GetUmbracoApiService<FoldersController>(x =>
                        x.GetFolderInfo(null)) },
                { "MoveFolder",
                    helper.GetUmbracoApiService<FoldersController>(x =>
                        x.MoveFolder(null)) },
                { "DeleteFolder",
                    helper.GetUmbracoApiService<FoldersController>(x =>
                        x.DeleteFolder(null)) },
                { "GetFieldTypes",
                    helper.GetUmbracoApiService<FieldsController>(x =>
                        x.GetFieldTypes()) },
                { "GetButtonKinds",
                    helper.GetUmbracoApiService<FieldsController>(x =>
                        x.GetButtonKinds()) },
                { "GetFieldCategories",
                    helper.GetUmbracoApiService<FieldsController>(x =>
                        x.GetFieldCategories()) },
                { "GetHandlerTypes",
                    helper.GetUmbracoApiService<HandlersController>(x =>
                        x.GetHandlerTypes()) },
                { "GetResultHandlers",
                    helper.GetUmbracoApiService<HandlersController>(x =>
                        x.GetResultHandlers()) },
                { "GetTemplates",
                    helper.GetUmbracoApiService<TemplatesController>(x =>
                        x.GetTemplates()) },
                { "PermitAccess",
                    helper.GetUmbracoApiService<SetupController>(x =>
                        x.PermitAccessToFormulate()) },
                { "GetEntityChildren",
                    helper.GetUmbracoApiService<EntitiesContentController>(x =>
                        x.GetEntityChildren(null)) },
                { "GetEntity",
                    helper.GetUmbracoApiService<EntitiesController>(x =>
                        x.GetEntity(null)) },
                { "GetSubmissions",
                    helper.GetUmbracoApiService<StoredDataController>(x =>
                        x.GetSubmissions(null)) },
                { "DownloadFile",
                    helper.GetUmbracoApiService<StoredDataDownloadController>(x =>
                        x.DownloadFile(null)) },
                { "DownloadCsvExport",
                    helper.GetUmbracoApiService<StoredDataDownloadController>(x =>
                        x.DownloadCsvExport(null)) },
                { "HasConfiguredRecaptcha",
                    helper.GetUmbracoApiService<ServerConfigurationController>(x =>
                        x.HasConfiguredRecaptcha()) },
                { "EditLayoutBase", "/formulate/formulate/editLayout/" },
                { "EditValidationBase",
                    "/formulate/formulate/editValidation/" },
                { "EditDataValueBase",
                    "/formulate/formulate/editDataValue/" },
                { "Layout.RootId", LayoutConstants.Id },
                { "Validation.RootId", ValidationConstants.Id },
                { "DataValue.RootId", DataValueConstants.Id },
                { "Form.RootId", FormConstants.Id }
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
            var needsUpgrade = !MetaConstants.Version.InvariantEquals(version);
            if (!isInstalled)
            {

                // Logging.
                LogHelper.Info<ApplicationStartedHandler>("Installing Formulate.");


                // Install Formulate.
                HandleInstall(applicationContext);

            }
            else if (needsUpgrade)
            {

                // Logging.
                LogHelper.Info<ApplicationStartedHandler>("Upgrading Formulate.");


                // Perform an upgrade installation.
                HandleInstall(applicationContext, true);

            }
        }


        /// <summary>
        /// Handles install operations.
        /// </summary>
        /// <param name="isUpgrade">
        /// Is this an upgrade to an existing instllation?
        /// </param>
        /// <param name="applicationContext">
        /// The current Umbraco application context.
        /// </param>
        private void HandleInstall(ApplicationContext applicationContext, bool isUpgrade = false)
        {

            // Add the Formulate section and the Formulate dashboard in the Formulate section.
            AddSection(applicationContext);
            AddDashboard();


            // If this is a new install, add the Formulate dashboard in the Developer section,
            // and check if users need to be given access to Formulate.
            if (!isUpgrade)
            {
                AddFormulateDeveloperTab();
                PermitAccess();
            }


            // Make changes to the web.config.
            AddConfigurationGroup();
            EnsureAppSettings();

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
        /// Indicates whether or not the application setting with the specified key has a non-empty
        /// value in the web.config.
        /// </summary>
        /// <param name="key">
        /// The application setting key.
        /// </param>
        /// <returns>
        /// True, if the value in the web.config is non-empty; otherwise, false.
        /// </returns>
        private bool DoesAppSettingExist(string key)
        {
            var value = ConfigurationManager.AppSettings[key];
            return !string.IsNullOrWhiteSpace(value);
        }


        /// <summary>
        /// Adds the Formulate section to Umbraco.
        /// </summary>
        /// <param name="applicationContext">
        /// The current application context.
        /// </param>
        private void AddSection(ApplicationContext applicationContext)
        {

            // Queue section change.
            QueueInstallAction(() =>
            {
                var service = applicationContext.Services.SectionService;
                var existingSection = service.GetByAlias("formulate");
                if (existingSection == null)
                {

                    // Logging.
                    LogHelper.Info<ApplicationStartedHandler>("Installing Formulate section in applications.config.");


                    // Variables.
                    var doc = new XmlDocument();
                    var actionXml = Resources.TransformApplications;
                    doc.LoadXml(actionXml);


                    // Add to applications.
                    PackageAction.RunPackageAction("Formulate",
                        "Formulate.TransformXmlFile", doc.FirstChild);


                    // Logging.
                    LogHelper.Info<ApplicationStartedHandler>("Done installing Formulate section in applications.config.");

                }
            });

        }


        /// <summary>
        /// Adds the Formulate dashboard to the Formulate section.
        /// </summary>
        private void AddDashboard()
        {

            // Queue dashboard transformation.
            QueueInstallAction(() =>
            {
                var exists = DashboardExists();
                if (!exists)
                {

                    // Logging.
                    LogHelper.Info<ApplicationStartedHandler>("Installing Formulate dashboard.");


                    // Variables.
                    var doc = new XmlDocument();
                    var actionXml = Resources.TransformDashboard;
                    doc.LoadXml(actionXml);


                    // Add dashboard.
                    PackageAction.RunPackageAction("Formulate",
                        "Formulate.TransformXmlFile", doc.FirstChild);

                    // Logging.
                    LogHelper.Info<ApplicationStartedHandler>("Done installing Formulate dashboard.");

                }
            });

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

            // Queue dashboard change.
            QueueInstallAction(() =>
            {
                var exists = FormulateDeveloperTabExists();
                if (!exists)
                {

                    // Logging.
                    LogHelper.Info<ApplicationStartedHandler>("Installing Formulate developer tab.");


                    // Variables.
                    var dashboardPath = SystemFiles.DashboardConfig;
                    var mappedDashboardPath = IOHelper.MapPath(dashboardPath);


                    // Get developer section from Dashboard.config.
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
                    dashboardDoc.Save(mappedDashboardPath);


                    // Logging.
                    LogHelper.Info<ApplicationStartedHandler>("Done installing Formulate developer tab.");

                }
            });

        }


        /// <summary>
        /// Adds or replaces the Formulate version number in the web.config, along with some other
        /// application settings.
        /// </summary>
        private void EnsureAppSettings()
        {

            // Queue the web.config change.
            QueueInstallAction(() =>
            {

                // Logging.
                LogHelper.Info<ApplicationStartedHandler>("Ensuring Formulate version in the web.config.");


                // Variables.
                var key = SettingConstants.VersionKey;
                var config = WebConfigurationManager.OpenWebConfiguration("~");
                var settings = config.AppSettings.Settings;
                var recaptchaKeys = new[]
                {
                    SettingConstants.RecaptchaSecretKey,
                    SettingConstants.RecaptchaSiteKey
                };


                // Replace the version setting.
                if (settings.AllKeys.Any(x => key.InvariantEquals(x)))
                {
                    settings.Remove(key);
                }
                settings.Add(key, MetaConstants.Version);


                // Ensure the Recaptcha keys exist in the web.config.
                foreach (var recaptchaKey in recaptchaKeys)
                {
                    if (!DoesAppSettingExist(recaptchaKey))
                    {
                        settings.Add(recaptchaKey, string.Empty);
                    }
                }


                // Save config changes.
                config.Save();


                // Logging.
                LogHelper.Info<ApplicationStartedHandler>("Done ensuring Formulate version in the web.config.");

            });

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
        /// Transforms the web.config to add the Formulate configuration group.
        /// </summary>
        private void AddConfigurationGroup()
        {

            // Queue web.config change to add Formulate configuration.
            QueueInstallAction(() =>
            {

                // Does the section group already exist and contain all the expected sections?
                var config = WebConfigurationManager.OpenWebConfiguration("~");
                var groupName = "formulateConfiguration";
                var group = config.GetSectionGroup(groupName);
                var exists = group != null;
                var sectionKeys = (group?.Sections?.Keys?.Cast<string>()?.ToArray()).MakeSafe();
                var sectionsSet = new HashSet<string>(sectionKeys);
                var expectedSections = new[]
                {
                    "buttons",
                    "emailWhitelist",
                    "fieldCategories",
                    "persistence",
                    "submissions",
                    "templates"
                };
                var containsAllSections = expectedSections.All(x => sectionsSet.Contains(x));


                // Only add the group if it doesn't exist or doesn't contain all the expected sections.
                if (!exists || !containsAllSections)
                {

                    // Logging.
                    LogHelper.Info<ApplicationStartedHandler>("Adding Formulate config to the web.config.");


                    // Variables.
                    var doc = new XmlDocument();
                    var actionXml = Resources.TransformWebConfig;
                    doc.LoadXml(actionXml);


                    // Add configuration group.
                    PackageAction.RunPackageAction("Formulate",
                        "Formulate.TransformXmlFile", doc.FirstChild);


                    // Logging.
                    LogHelper.Info<ApplicationStartedHandler>("Done adding Formulate config to the web.config.");

                }

            });

        }


        /// <summary>
        /// Queues an install action to be run in a few seconds.
        /// </summary>
        /// <param name="action">
        /// The install action.
        /// </param>
        private void QueueInstallAction(Action action)
        {
            lock (InstallActionsLock)
            {
                InstallActions.Add(action);
                if (InstallTimer == null)
                {
                    var twentySeconds = 1000 * 20;
                    InstallTimer = new Timer(twentySeconds);
                    InstallTimer.AutoReset = false;
                    InstallTimer.Elapsed += HandleInstallTimerElapsed;
                    InstallTimer.Start();
                }
            }
        }


        /// <summary>
        /// Once the install timer elapses, run the install actions.
        /// </summary>
        private void HandleInstallTimerElapsed(object sender, ElapsedEventArgs e)
        {

            // Logging.
            LogHelper.Info<ApplicationStartedHandler>("Running the queue of Formulate install actions.");


            // Queueing this way should avoid application pool recycles during the install process,
            // which will ensure that only one application pool recyle occurs, even if there are multiple
            // configuration changes made.
            HostingEnvironment.QueueBackgroundWorkItem(token =>
            {
                lock (InstallActionsLock)
                {
                    try
                    {
                        InstallActions.ForEach(x =>
                            x()
                        );
                    }
                    catch (Exception ex)
                    {
                        LogHelper.Error<ApplicationStartedHandler>(InstallActionsError, ex);
                    }
                    finally
                    {

                        // Reset queue.
                        InstallTimer = null;
                        InstallActions = new List<Action>();


                        // Logging.
                        LogHelper.Info<ApplicationStartedHandler>("Done running the queue of Formulate install actions.");

                    }
                }
            });

        }

        #endregion

    }

}