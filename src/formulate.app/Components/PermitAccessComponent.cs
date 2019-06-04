namespace formulate.app.Components
{
    using System;
    using System.Configuration;
    using System.Linq;

    using Umbraco.Core.Composing;
    using Umbraco.Core.Logging;
    using Umbraco.Core.Models.Membership;
    using Umbraco.Core.Models.Sections;
    using Umbraco.Core.Services;
    using Umbraco.Web.Services;

    using SettingConstants = core.Constants.Settings;

    /// <summary>
    /// The permit access component.
    /// </summary>
    internal sealed class PermitAccessComponent : IComponent
    {
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
        /// Initializes a new instance of the <see cref="PermitAccessComponent"/> class.
        /// </summary>
        /// <param name="sectionService">
        /// The section service.
        /// </param>
        /// <param name="userService">
        /// The user service.
        /// </param>
        /// <param name="logger">
        /// The logger.
        /// </param>
        public PermitAccessComponent(ISectionService sectionService, IUserService userService, ILogger logger)
        {
            SectionService = sectionService;
            UserService = userService;
            Logger = logger;
        }

        /// <summary>
        /// Runs the code for adding the Formulate section to user groups.
        /// </summary>
        public void Initialize()
        {
            // Variables.
            var key = SettingConstants.EnsureUsersCanAccess;
            var ensure = ConfigurationManager.AppSettings[key];

            // Should all users be given access to Formulate?
            if (string.IsNullOrWhiteSpace(ensure))
            {
                Logger.Info<PermitAccessComponent>("Skipping permit access. Ensure User Can Access is not present");
                return;
            }

            var formulateSection = SectionService.GetByAlias("formulate");

            if (formulateSection == null)
            {
                Logger.Warn<PermitAccessComponent>("Skipping permit access. Formulate section was not found.");
                return;
            }

            var adminUserGroup = UserService.GetUserGroupByAlias(Umbraco.Core.Constants.Security.AdminGroupAlias);

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
                Logger.Warn<PermitAccessComponent>(
                    $"Skipping permit access. No user group was found with alias: {Umbraco.Core.Constants.Security.AdminGroupAlias}.");
                return;
            }

            if (userGroup.AllowedSections.Contains(formulateSection.Alias))
            {
                Logger.Info<PermitAccessComponent>(
                    $"Skipping permit access. {formulateSection.Name} Section already exists on User Group, {userGroup.Name}.");
                return;
            }

            try
            {
                userGroup.AddAllowedSection(formulateSection.Alias);
                UserService.Save(userGroup);

                Logger.Info<PermitAccessComponent>(
                    $"Successfully added {formulateSection.Name} Section to User Group, {userGroup.Name}.");
            }
            catch (Exception ex)
            {
                Logger.Error<PermitAccessComponent>(ex, $"Error adding {formulateSection.Name} Section to User Group, {userGroup.Name}.");
            }
        }

        /// <summary>
        /// Run during component termination.
        /// </summary>
        public void Terminate()
        {
        }
    }
}
