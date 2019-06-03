namespace formulate.app.Components
{
    using formulate.app.Migrations;
    using Umbraco.Core.Composing;
    using Umbraco.Core.Logging;
    using Umbraco.Core.Migrations;
    using Umbraco.Core.Migrations.Upgrade;
    using Umbraco.Core.Scoping;
    using Umbraco.Core.Services;

    /// <summary>
    /// The install database migration component.
    /// </summary>
    internal sealed class InstallDatabaseMigrationComponent : IComponent
    {
        /// <summary>
        /// Gets or sets the scope provider.
        /// </summary>
        private IScopeProvider ScopeProvider { get; set; }

        /// <summary>
        /// Gets or sets the migration builder.
        /// </summary>
        private IMigrationBuilder MigrationBuilder { get; set; }

        /// <summary>
        /// Gets or sets the key value service.
        /// </summary>
        private IKeyValueService KeyValueService { get; set; }

        /// <summary>
        /// Gets or sets the logger.
        /// </summary>
        private ILogger Logger { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="InstallDatabaseMigrationComponent"/> class.
        /// </summary>
        /// <param name="scopeProvider">
        /// The scope provider.
        /// </param>
        /// <param name="migrationBuilder">
        /// The migration builder.
        /// </param>
        /// <param name="keyValueService">
        /// The key value service.
        /// </param>
        /// <param name="logger">
        /// The logger.
        /// </param>
        public InstallDatabaseMigrationComponent(IScopeProvider scopeProvider, IMigrationBuilder migrationBuilder, IKeyValueService keyValueService, ILogger logger)
        {
            ScopeProvider = scopeProvider;
            MigrationBuilder = migrationBuilder;
            KeyValueService = keyValueService;
            Logger = logger;
        }

        /// <summary>
        /// Runs the inital migration.
        /// </summary>
        public void Initialize()
        {
            var plan = new MigrationPlan("Formulate");
            plan.From(string.Empty)
                .To<CreateSubmissionsTable>("state-1");

            var upgrader = new Upgrader(plan);
            upgrader.Execute(ScopeProvider, MigrationBuilder, KeyValueService, Logger);
        }

        /// <summary>
        /// Fires after the component has run.
        /// </summary>
        public void Terminate()
        {
        }
    }
}
