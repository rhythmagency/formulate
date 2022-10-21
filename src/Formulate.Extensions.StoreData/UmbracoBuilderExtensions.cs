namespace Formulate.Extensions.StoreData
{
    using Formulate.Extensions.StoreData.NotificationHandlers;
    using Umbraco.Cms.Core.DependencyInjection;
    using Umbraco.Cms.Core.Notifications;

    internal static class UmbracoBuilderExtensions
    {
        public static IUmbracoBuilder AddPackageManifest(this IUmbracoBuilder umbracoBuilder)
        {
            if (umbracoBuilder.ManifestFilters().Has<PackageManifestFilter>())
            {
                return umbracoBuilder;
            }

            umbracoBuilder.ManifestFilters().Append<PackageManifestFilter>();

            return umbracoBuilder;
        }

        public static IUmbracoBuilder RunMigrations(this IUmbracoBuilder umbracoBuilder)
        {
            umbracoBuilder.AddNotificationHandler<UmbracoApplicationStartingNotification, RunMigrationsNotificationHandler>();

            return umbracoBuilder;
        }
    }
}
