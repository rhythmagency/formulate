using Formulate.Core.FormFields;
using Formulate.Core.FormHandlers;
using Formulate.Core.Validations;
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

        /// <summary>
        /// Adds Formulate collection builders to Umbraco.
        /// </summary>
        /// <param name="builder">The Umbraco builder.</param>
        /// <returns>The current <see cref="IUmbracoBuilder"/>.</returns>
        private static IUmbracoBuilder AddFormulateCollections(this IUmbracoBuilder builder)
        {
            builder.DataValuesTypes();

            builder.FormFieldTypes().Add(() => builder.TypeLoader.GetTypes<FormFieldType>());
            
            builder.FormHandlerTypes().Add(() => builder.TypeLoader.GetTypes<AsyncFormHandlerType>());
            
            builder.FormHandlerTypes().Add(() => builder.TypeLoader.GetTypes<FormHandlerType>());

            builder.LayoutTypes();

            builder.ValidationTypes().Add(() => builder.TypeLoader.GetTypes<IValidationType>()); ;

            return builder;
        }
    }
}
