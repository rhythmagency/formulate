namespace Formulate.BackOffice.StaticAssets
{
    using Umbraco.Cms.Core.DependencyInjection;

    public static class UmbracoBuilderExtensions
    {
        public static IUmbracoBuilder AddFormulateBackOfficeStaticAssets(this IUmbracoBuilder umbracoBuilder)
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
