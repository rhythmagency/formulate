namespace formulate.app.Composers
{
    using formulate.app.Backoffice;
    using formulate.app.Backoffice.Dashboards;
    using formulate.app.Components;

    using Umbraco.Core;
    using Umbraco.Core.Composing;
    using Umbraco.Web;

    /// <summary>
    /// The initialize formulate user composer.
    /// </summary>
    [RuntimeLevel(MinLevel = RuntimeLevel.Run)]
    [ComposeAfter(typeof(CompositionRegistryUserComposer))]
    public sealed class InitializeFormulateUserComposer : IUserComposer
    {
        /// <summary>
        /// Composes features of Formulate that must run after Umbraco has been installed.
        /// </summary>
        /// <param name="composition">
        /// The composition.
        /// </param>
        public void Compose(Composition composition)
        {
            InitializeDatabase(composition);
            InitializeBackoffice(composition);
            InitializeServerVariables(composition);
            HandleInstallAndUpgrade(composition);
        }

        /// <summary>
        /// Add the Formulate section and the Formulate dashboards.
        /// </summary>
        /// <param name="composition">The composition.</param>
        private void InitializeBackoffice(Composition composition)
        {
            AddSection(composition);
            AddFormulateDashboard(composition);
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
            composition.Components().Append<HandleInstallAndUpgradeComponent>();
        }
    }
}
