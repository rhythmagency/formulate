using Umbraco.Cms.Core.DependencyInjection;

namespace Formulate.Core.DependencyInjection
{
    /// <summary>
    /// Extension methods that augment <see cref="IUmbracoBuilder"/>.
    /// </summary>
    public static partial class UmbracoBuilderExtensions
    {
        /// <summary>
        /// Adds Formulate core logic to Umbraco.
        /// </summary>
        /// <param name="builder">The Umbraco builder.</param>
        /// <returns>The current <see cref="IUmbracoBuilder"/>.</returns>
        public static IUmbracoBuilder AddFormulateCore(this IUmbracoBuilder builder)
        {
            builder.AddFormulateCollections()
                   .AddFormulateUtilities()
                   .AddFormulateFactories();

            return builder;
        }
    }
}
