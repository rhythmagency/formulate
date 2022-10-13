namespace Formulate.Core.DependencyInjection
{
    // Namespaces.
    using DataValues;
    using Extensions;
    using FormFields;
    using FormHandlers;
    using Layouts;
    using System.Linq;
    using Templates;
    using Umbraco.Cms.Core.DependencyInjection;
    using Validations;

    public static partial class UmbracoBuilderExtensions
    {
        /// <summary>
        /// Gets the builder collection for adding <see cref="IDataValuesDefinition"/> implementations.
        /// </summary>
        /// <param name="builder">The Umbraco builder.</param>
        /// <returns>A <see cref="DataValuesDefinitionCollectionBuilder"/>.</returns>
        public static DataValuesDefinitionCollectionBuilder DataValuesDefinitions(this IUmbracoBuilder builder)
        {
            return builder.WithCollectionBuilder<DataValuesDefinitionCollectionBuilder>();
        }

        /// <summary>
        /// Gets the builder collection for adding <see cref="IFormFieldDefinition"/> implementations.
        /// </summary>
        /// <param name="builder">The Umbraco builder.</param>
        /// <returns>A <see cref="FormFieldDefinitionCollectionBuilder"/>.</returns>
        public static FormFieldDefinitionCollectionBuilder
            FormFieldDefinitions(this IUmbracoBuilder builder)
        {
            return builder
                .WithCollectionBuilder<FormFieldDefinitionCollectionBuilder>();
        }

        /// <summary>
        /// Gets the builder collection for adding <see cref="IFormHandlerDefinition"/> implementations.
        /// </summary>
        /// <param name="builder">The Umbraco builder.</param>
        /// <returns>A <see cref="LayoutDefinitionCollectionBuilder"/>.</returns>
        public static FormHandlerDefinitionCollectionBuilder FormHandlerDefinitions(this IUmbracoBuilder builder)
        {
            return builder.WithCollectionBuilder<FormHandlerDefinitionCollectionBuilder>();
        }

        /// <summary>
        /// Gets the builder collection for adding <see cref="ILayoutDefinition"/> implementations.
        /// </summary>
        /// <param name="builder">The Umbraco builder.</param>
        /// <returns>A <see cref="LayoutDefinitionCollectionBuilder"/>.</returns>
        public static LayoutDefinitionCollectionBuilder LayoutDefinitions(this IUmbracoBuilder builder)
        {
            return builder.WithCollectionBuilder<LayoutDefinitionCollectionBuilder>();
        }

        /// <summary>
        /// Gets the builder collection for adding <see cref="ITemplateDefinition"/> implementations.
        /// </summary>
        /// <param name="builder">The Umbraco builder.</param>
        /// <returns>A <see cref="TemplateDefinitionCollectionBuilder"/>.</returns>
        public static TemplateDefinitionCollectionBuilder TemplateDefinitions(this IUmbracoBuilder builder)
        {
            return builder.WithCollectionBuilder<TemplateDefinitionCollectionBuilder>();
        }



        /// <summary>
        /// Gets the builder collection for adding <see cref="IValidationDefinition"/> implementations.
        /// </summary>
        /// <param name="builder">The Umbraco builder.</param>
        /// <returns>A <see cref="ValidationDefinitionCollectionBuilder"/>.</returns>
        public static ValidationDefinitionCollectionBuilder ValidationDefinitions(this IUmbracoBuilder builder)
        {
            return builder.WithCollectionBuilder<ValidationDefinitionCollectionBuilder>();
        }

        /// <summary>
        /// Adds Formulate collection builders to Umbraco.
        /// </summary>
        /// <param name="builder">The Umbraco builder.</param>
        /// <returns>The current <see cref="IUmbracoBuilder"/>.</returns>
        private static IUmbracoBuilder AddFormulateCollections(this IUmbracoBuilder builder)
        {
            builder.DataValuesDefinitions().Add(() =>
                builder.TypeLoader.GetTypes<IDataValuesDefinition>());

            var fieldDefinitionType = typeof(FormFieldDefinitionBase<>);
            builder.FormFieldDefinitions().Add(() =>
                builder.TypeLoader
                    .GetTypes<IFormFieldDefinition>()
                    .Where(x => x.IsTypeDerivedFromGenericType(fieldDefinitionType)));

            builder.FormHandlerDefinitions().Add(() =>
                builder.TypeLoader.GetTypes<FormHandlerDefinition>());

            builder.LayoutDefinitions().Add(() =>
                builder.TypeLoader.GetTypes<ILayoutDefinition>());

            builder.ValidationDefinitions().Add(() =>
                builder.TypeLoader.GetTypes<IValidationDefinition>());

            builder.TemplateDefinitions();

            return builder;
        }
    }
}