namespace formulate.app.Composers
{
    using formulate.app.Components;

    using Umbraco.Core;
    using Umbraco.Core.Composing;

    [RuntimeLevel(MinLevel = RuntimeLevel.Run)]
    public sealed class PostUmbracoInstallComposer : IUserComposer
    {
        /// <summary>
        /// Registers database migrations.
        /// </summary>
        /// <param name="composition">
        /// The composition.
        /// </param>
        public void Compose(Composition composition)
        {
            InitializeDatabase(composition);
            HandleInstallAndUpgrade(composition);
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
