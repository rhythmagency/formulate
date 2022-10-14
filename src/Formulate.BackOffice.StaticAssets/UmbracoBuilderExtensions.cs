namespace Formulate.BackOffice.StaticAssets
{
    using Umbraco.Cms.Core.DependencyInjection;

    public static class UmbracoBuilderExtensions
    {
        private static IUmbracoBuilder AddPackageManifest(this IUmbracoBuilder umbracoBuilder)
        {
            if (umbracoBuilder.ManifestFilters().Has<PackageManifestFilter>())
            {
                return umbracoBuilder;
            }

            umbracoBuilder.ManifestFilters().Append<PackageManifestFilter>();

            return umbracoBuilder;
        }

        /// <summary>
        /// Adds the Formulate BackOffice static assets to the current <see cref="IUmbracoBuilder"/>.
        /// </summary>
        /// <param name="umbracoBuilder">The umbraco builder.</param>
        /// <returns>A <see cref="IUmbracoBuilder"/>.</returns>
        public static IUmbracoBuilder AddFormulateBackOfficeStaticAssets(this IUmbracoBuilder umbracoBuilder)
        {
            umbracoBuilder.AddPackageManifest();
         
            return umbracoBuilder;
        }
    }
}
