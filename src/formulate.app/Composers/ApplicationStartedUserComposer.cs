namespace formulate.app.Composers
{
    // Namespaces.
    using System.Configuration;
    using System.Xml;

    using formulate.app.Backoffice;
    using formulate.app.Backoffice.Dashboards;
    using formulate.app.Components;
    using formulate.app.Configuration;
    using formulate.app.Constants.Configuration;
    using formulate.app.ExtensionMethods;

    using Umbraco.Core;
    using Umbraco.Core.Composing;
    using Umbraco.Web;

    using MetaConstants = meta.Constants;
    using Resources = Properties.Resources;
    using SettingConstants = core.Constants.Settings;

    /// <summary>
    /// Handles the application started event.
    /// </summary>
    [RuntimeLevel(MinLevel = RuntimeLevel.Run)]
    public class ApplicationStartedUserComposer : IUserComposer
    {
        #region Constants

        private const string DeveloperSectionXPath = @"/dashBoard/section[@alias='StartupDeveloperDashboardSection']";
        private const string MissingDeveloperSection = @"Unable to locate StartupDeveloperDashboardSection in the dashboard.config. The Formulate tab will not be added to the Developer section.";
        private const string InstallActionsError = @"An unknown error occurred while attempting to asynchronously run the install actions for Formulate.";
        private const string TableCreationError = @"An error occurred while attempting to create the FormulateSubmissions table.";

        #endregion

        #region Methods

        /// <summary>
        /// The compose.
        /// </summary>
        /// <param name="composition">
        /// The composition.
        /// </param>
        public void Compose(Composition composition)
        {
            InitializeConfiguration(composition);
            HandleInstallAndUpgrade(composition);
            InitializeDatabase(composition);
            InitializeServerVariables(composition);
        }

        /// <summary>
        /// The initialize configuration.
        /// </summary>
        /// <param name="composition">
        /// The composition.
        /// </param>
        private void InitializeConfiguration(Composition composition)
        {
            composition.Configs.AddJsonConfig<IFormulateConfig, FormulateConfig>(ConfigFilePaths.FormulateConfigPath);
        }

        /// <summary>
        /// The initialize server variables.
        /// </summary>
        /// <param name="composition">
        /// The composition.
        /// </param>
        private void InitializeServerVariables(Composition composition)
        {
            composition.Components().Append<ServerVariablesComponent>();
        }

        /// <summary>
        /// Modifies the database (e.g., adding necessary tables).
        /// </summary>
        /// <param name="composition">
        /// The composition.
        /// </param>
        private void InitializeDatabase(Composition composition)
        {
            composition.Components().Append<InstallDatabaseMigrationComponent>();
        }

        /// <summary>
        /// Handles install and upgrade operations.
        /// </summary>
        /// <param name="composition">
        /// The composition.
        /// </param>
        private void HandleInstallAndUpgrade(Composition composition)
        {
            var version = GetInstalledVersion(composition);
            var isInstalled = string.IsNullOrWhiteSpace(version) == false;
            var needsUpgrade = !MetaConstants.Version.InvariantEquals(version);

            if (!isInstalled)
            {
                // Install Formulate.
                HandleInstall(composition);
            }
            else if (needsUpgrade)
            {
                // Perform an upgrade installation.
                HandleInstall(composition, true);
            }
        }

        /// <summary>
        /// Handles install operations.
        /// </summary>
        /// <param name="composition">
        /// The composition.
        /// </param>
        /// <param name="isUpgrade">
        /// Is this an upgrade to an existing installation?
        /// </param>
        private void HandleInstall(Composition composition, bool isUpgrade = false)
        {
            // Add the Formulate section and the Formulate dashboard in the Formulate section.
            AddSection(composition);
            AddFormulateDashboard(composition);

            // If this is a new install, add the Formulate dashboard in the Developer section,
            // and check if users need to be given access to Formulate.
            if (!isUpgrade)
            {
                AddFormulateDeveloperDashboard(composition);
                PermitAccess(composition);
                UpdateVersion(composition);
            }

            // Make changes to the web.config.
            EnsureAppSettings();
        }

        private void UpdateVersion(Composition composition)
        {
            composition.Components().Append<UpdateVersionComponent>();
        }

        /// <summary>
        /// Gets the installed version.
        /// </summary>
        /// <param name="composition">
        /// The composition.
        /// </param>
        /// <returns>
        /// The installed version, or null.
        /// </returns>
        private string GetInstalledVersion(Composition composition)
        {
            var config = composition.Configs.GetConfig<IFormulateConfig>();

            return config?.Version;
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
        /// <param name="composition">
        /// The composition.
        /// </param>
        private void AddSection(Composition composition)
        {
            composition.Sections().Append<FormulateSection>();
        }

        /// <summary>
        /// Adds the Formulate dashboard to the Formulate section.
        /// </summary>
        /// <param name="composition">
        /// The composition.
        /// </param>
        private void AddFormulateDashboard(Composition composition)
        {
            composition.Dashboards().Add<FormulateDashboard>();
        }

        /// <summary>
        /// Adds the "Formulate" tab to the developer section of the dashboard.config.
        /// </summary>
        /// <param name="composition">
        /// The composition.
        /// </param>
        private void AddFormulateDeveloperDashboard(Composition composition)
        {
            composition.Dashboards().Add<FormulateDeveloperDashboard>();
        }


        /// <summary>
        /// Adds or replaces the Formulate version number in the web.config, along with some other
        /// application settings.
        /// </summary>
        private void EnsureAppSettings()
        {

            // Queue the web.config change.
            //QueueInstallAction(() =>
            //{

            //    // Logging.
            //    Logger.Info<ApplicationStartedUserComposer>("Ensuring Formulate version in the web.config.");


            //    // Variables.
            //    var key = SettingConstants.VersionKey;
            //    var config = WebConfigurationManager.OpenWebConfiguration("~");
            //    var settings = config.AppSettings.Settings;
            //    var formulateKeys = new[]
            //    {
            //        new
            //        {
            //            key = SettingConstants.RecaptchaSiteKey,
            //            value = string.Empty
            //        },

            //        new {
            //          key = SettingConstants.RecaptchaSecretKey,
            //          value = string.Empty
            //        },

            //        new {
            //            key = SettingConstants.EnableJSONFormLogging,
            //            value = "false"
            //        }
            //    };



            //    // Replace the version setting.
            //    if (settings.AllKeys.Any(x => key.InvariantEquals(x)))
            //    {
            //        settings.Remove(key);
            //    }
            //    settings.Add(key, MetaConstants.Version);


            //    // Ensure the Recaptcha keys exist in the web.config.
            //    foreach (var configKey in formulateKeys)
            //    {
            //        if (!DoesAppSettingExist(configKey.key))
            //        {
            //            settings.Add(configKey.key, configKey.value);
            //        }
            //    }


            //    // Save config changes.
            //    config.Save();


            //    // Logging.
            //    Logger.Info<ApplicationStartedUserComposer>("Done ensuring Formulate version in the web.config.");

            //});

        }

        /// <summary>
        /// Permits all users to access Formulate if configured in the web.config.
        /// </summary>
        /// <param name="composition"></param>
        private void PermitAccess(Composition composition)
        {
            composition.Components().Append<PermitAccessComponent>();

        }
        #endregion
    }
}