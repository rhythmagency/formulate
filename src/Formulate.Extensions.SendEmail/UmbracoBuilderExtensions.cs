namespace Formulate.Extensions.SendEmail
{
    using Umbraco.Cms.Core.DependencyInjection;

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
    }
}
