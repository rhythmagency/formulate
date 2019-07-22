namespace formulate.app.Components
{
    using System;
    using System.IO;
    using System.Linq;

    using formulate.app.Configuration;
    using formulate.app.Constants.Configuration;

    using Newtonsoft.Json;

    using Umbraco.Core;
    using Umbraco.Core.Composing;
    using Umbraco.Core.Configuration;
    using Umbraco.Core.IO;
    using Umbraco.Core.Logging;
    using Umbraco.Core.Models.Membership;
    using Umbraco.Core.Models.Sections;
    using Umbraco.Core.Services;
    using Umbraco.Web.Services;

    /// <summary>
    /// The handle install and upgrade component.
    /// </summary>
    internal sealed class HandleInstallAndUpgradeComponent : IComponent
    {
        /// <summary>
        /// Gets or sets the config.
        /// </summary>
        private IFormulateConfig Config { get; set; }

        /// <summary>
        /// Gets or sets the user service.
        /// </summary>
        private IUserService UserService { get; set; }

        /// <summary>
        /// Gets or sets the section service.
        /// </summary>
        private ISectionService SectionService { get; set; }

        /// <summary>
        /// Gets or sets the logger.
        /// </summary>
        private ILogger Logger { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="HandleInstallAndUpgradeComponent"/> class.
        /// </summary>
        /// <param name="config">
        /// The config.
        /// </param>
        /// <param name="sectionService">
        /// The section service.
        /// </param>
        /// <param name="userService">
        /// The user service.
        /// </param>
        /// <param name="logger">
        /// The logger.
        /// </param>
        public HandleInstallAndUpgradeComponent(IFormulateConfig config, ISectionService sectionService, IUserService userService, ILogger logger)
        {
            Config = config;
            SectionService = sectionService;
            UserService = userService;
            Logger = logger;
        }

        /// <summary>
        /// Runs upon component initialization.
        /// </summary>
        public void Initialize()
        {
            var version = GetConfiguredVersion();
            var isNewInstall = string.IsNullOrWhiteSpace(version);
            var isDifferentVersion = (isNewInstall && meta.Constants.Version.InvariantEquals(version)) == false;

            if (isNewInstall)
            {
                PermitAccessToFormulateForAdministrators();
            }

            if (isNewInstall || isDifferentVersion)
            {
                UpdateConfiguredVersion();
            }
        }

        /// <summary>
        /// Permits access to Formulate for administrators.
        /// </summary>
        private void PermitAccessToFormulateForAdministrators()
        {
            var formulateSection = SectionService.GetByAlias("formulate");

            if (formulateSection == null)
            {
                Logger.Warn<HandleInstallAndUpgradeComponent>("Skipping permit access. Formulate section was not found.");
                return;
            }

            var adminUserGroup = UserService.GetUserGroupByAlias(Constants.Security.AdminGroupAlias);
            AddSectionToUserGroup(formulateSection, adminUserGroup);
        }

        /// <summary>
        /// The add section to user group.
        /// </summary>
        /// <param name="formulateSection">
        /// The formulate section.
        /// </param>
        /// <param name="userGroup">
        /// The user group.
        /// </param>
        private void AddSectionToUserGroup(ISection formulateSection, IUserGroup userGroup)
        {
            if (userGroup == null)
            {
                Logger.Warn<HandleInstallAndUpgradeComponent>(
                    $"Skipping permit access. No user group was found.");
                return;
            }

            if (userGroup.AllowedSections.Contains(formulateSection.Alias))
            {
                Logger.Info<HandleInstallAndUpgradeComponent>(
                    $"Skipping permit access. {formulateSection.Name} Section already exists on User Group, {userGroup.Name}.");
                return;
            }

            try
            {
                userGroup.AddAllowedSection(formulateSection.Alias);
                UserService.Save(userGroup);

                Logger.Info<HandleInstallAndUpgradeComponent>(
                    $"Successfully added {formulateSection.Name} Section to User Group, {userGroup.Name}.");
            }
            catch (Exception ex)
            {
                Logger.Error<HandleInstallAndUpgradeComponent>(ex, $"Error adding {formulateSection.Name} Section to User Group, {userGroup.Name}.");
            }
        }


        /// <summary>
        /// Gets the configured version of Formulate.
        /// </summary>
        /// <returns>
        /// The installed version, or null.
        /// </returns>
        private string GetConfiguredVersion()
        {
            return Config?.Version;
        }

        /// <summary>
        /// Updates the configured version of Formulate.
        /// </summary>
        private void UpdateConfiguredVersion()
        {
            var jsonConfig = Config as FormulateConfig;

            if (jsonConfig == null)
            {
                return;
            }

            jsonConfig.Version = meta.Constants.Version;

            SaveToFileSystem(jsonConfig, ConfigFilePaths.FormulateConfigPath);
        }

        /// <summary>
        /// Updates the JSON file system with a newer version of the config.
        /// </summary>
        /// <param name="config">
        /// The config.
        /// </param>
        /// <param name="filePath">
        /// The file path.
        /// </param>
        /// <exception cref="Exception">In case of any issues saving to file.</exception>
        private void SaveToFileSystem(FormulateConfig config, string filePath)
        {
            var mappedPath = IOHelper.MapPath(filePath);

            if (File.Exists(mappedPath))
            {
                try
                {
                    using (var file = File.CreateText(mappedPath))
                    {
                        var serializer = new JsonSerializer();
                        serializer.Serialize(file, config);

                        Logger.Info<HandleInstallAndUpgradeComponent>($"Updated configured Formulate version to {config.Version}.");
                    }
                }
                catch (Exception ex)
                {
                    Logger.Error<Configs>(ex, "Unable to save Formulate Config to file system.");
                }
            }
        }

        /// <summary>
        /// Runs upon component terminatation.
        /// </summary>
        public void Terminate()
        {
        }
    }
}
