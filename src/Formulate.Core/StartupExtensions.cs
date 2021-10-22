using Formulate.Core.Types;
using Umbraco.Cms.Core.DependencyInjection;

namespace Formulate.Core
{
    /// <summary>
    /// Extension methods that augment startup.
    /// </summary>
    public static class StartupExtensions
    {
        /// <summary>
        /// Adds Formulate core logic to Umbraco.
        /// </summary>
        /// <param name="builder">The Umbraco builder.</param>
        /// <returns>The current <see cref="IUmbracoBuilder"/>.</returns>
        public static IUmbracoBuilder AddFormulateCore(this IUmbracoBuilder builder)
        {
            builder.AddFormulateCollections();

            return builder;
        }

        /// <summary>
        /// Adds Formulate collection builders to Umbraco.
        /// </summary>
        /// <param name="builder">The Umbraco builder.</param>
        /// <returns>The current <see cref="IUmbracoBuilder"/>.</returns>
        private static IUmbracoBuilder AddFormulateCollections(this IUmbracoBuilder builder)
        {
            builder.DataValuesTypes();
            builder.FormFieldTypes();
            builder.FormHandlerTypes();
            builder.LayoutTypes();

            return builder;
        }
    }
}
