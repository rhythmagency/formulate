namespace Formulate.Extensions.StoreData.NotificationHandlers
{
    using Umbraco.Cms.Core.Migrations;
    using Umbraco.Cms.Core.Notifications;
    using Umbraco.Cms.Core.Scoping;
    using Umbraco.Cms.Core.Services;
    using Umbraco.Cms.Core;
    using Umbraco.Cms.Infrastructure.Migrations.Upgrade;
    using Umbraco.Cms.Infrastructure.Migrations;
    using Constants = Formulate.Extensions.StoreData.Constants;
    using Formulate.Extensions.StoreData.Migrations;
    using Umbraco.Cms.Core.Events;

    internal sealed class RunMigrationsNotificationHandler : INotificationHandler<UmbracoApplicationStartingNotification>
    {
        private readonly IMigrationPlanExecutor _migrationPlanExecutor;
        private readonly ICoreScopeProvider _coreScopeProvider;
        private readonly IKeyValueService _keyValueService;
        private readonly IRuntimeState _runtimeState;

        public RunMigrationsNotificationHandler(
            ICoreScopeProvider coreScopeProvider,
            IMigrationPlanExecutor migrationPlanExecutor,
            IKeyValueService keyValueService,
            IRuntimeState runtimeState)
        {
            _migrationPlanExecutor = migrationPlanExecutor;
            _coreScopeProvider = coreScopeProvider;
            _keyValueService = keyValueService;
            _runtimeState = runtimeState;
        }

        public void Handle(UmbracoApplicationStartingNotification notification)
        {
            if (_runtimeState.Level < RuntimeLevel.Run)
            {
                return;
            }

            // Create a migration plan for a specific project/feature
            // We can then track that latest migration state/step for this project/feature
            var migrationPlan = new MigrationPlan(Constants.Package.FullName);

            // This is the steps we need to take
            // Each step in the migration adds a unique value
            migrationPlan.From(string.Empty)
                .To<AddTable>(nameof(AddTable));

            // Go and upgrade our site (Will check if it needs to do the work or not)
            // Based on the current/latest step
            var upgrader = new Upgrader(migrationPlan);
            upgrader.Execute(
                _migrationPlanExecutor,
                _coreScopeProvider,
                _keyValueService);
        }
    }
}
