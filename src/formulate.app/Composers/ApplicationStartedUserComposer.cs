namespace formulate.app.Composers
{
    // Namespaces.
    using System.Configuration;

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
            InitializeBackoffice(composition);
            HandleInstallAndUpgrade(composition);
            InitializeDatabase(composition);
            InitializeServerVariables(composition);
        }

        /// <summary>
        /// Add the Formulate section and the Formulate dashboards.
        /// </summary>
        /// <param name="composition">The composition.</param>
        private void InitializeBackoffice(Composition composition)
        {
            AddSection(composition);
            AddFormulateDashboard(composition);
            AddFormulateDeveloperDashboard(composition);
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
            var isNewInstall = string.IsNullOrWhiteSpace(version);
            var isDifferentVersion = (isNewInstall && MetaConstants.Version.InvariantEquals(version)) == false;

            if (isNewInstall)
            {
                PermitAccess(composition);
            }

            if (isNewInstall || isDifferentVersion)
            {
                UpdateVersion(composition);
            }
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