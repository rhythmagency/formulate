using Formulate.Core.DataValues;
using Formulate.Core.FormFields;
using Formulate.Core.FormHandlers;
using Formulate.Core.Layouts;
using Formulate.Core.Validations;
using Umbraco.Cms.Core.DependencyInjection;

namespace Formulate.Core.DependencyInjection
{
    public static partial class UmbracoBuilderExtensions
    {
        /// <summary>
        /// Gets the builder collection for adding <see cref="IDataValuesType"/> implementations.
        /// </summary>
        /// <param name="builder">The Umbraco builder.</param>
        /// <returns>A <see cref="DataValuesTypeCollectionBuilder"/>.</returns>
        public static DataValuesTypeCollectionBuilder DataValuesTypes(this IUmbracoBuilder builder)
        {
            return builder.WithCollectionBuilder<DataValuesTypeCollectionBuilder>();
        }

        /// <summary>
        /// Gets the builder collection for adding <see cref="IFormFieldType"/> implementations.
        /// </summary>
        /// <param name="builder">The Umbraco builder.</param>
        /// <returns>A <see cref="FormFieldTypeCollectionBuilder"/>.</returns>
        public static FormFieldTypeCollectionBuilder FormFieldTypes(this IUmbracoBuilder builder)
        {
            return builder.WithCollectionBuilder<FormFieldTypeCollectionBuilder>();
        }

        /// <summary>
        /// Gets the builder collection for adding <see cref="IFormHandlerType"/> implementations.
        /// </summary>
        /// <param name="builder">The Umbraco builder.</param>
        /// <returns>A <see cref="LayoutTypeCollectionBuilder"/>.</returns>
        public static FormHandlerTypeCollectionBuilder FormHandlerTypes(this IUmbracoBuilder builder)
        {
            return builder.WithCollectionBuilder<FormHandlerTypeCollectionBuilder>();
        }

        /// <summary>
        /// Gets the builder collection for adding <see cref="ILayoutType"/> implementations.
        /// </summary>
        /// <param name="builder">The Umbraco builder.</param>
        /// <returns>A <see cref="LayoutTypeCollectionBuilder"/>.</returns>
        public static LayoutTypeCollectionBuilder LayoutTypes(this IUmbracoBuilder builder)
        {
            return builder.WithCollectionBuilder<LayoutTypeCollectionBuilder>();
        }

        /// <summary>
        /// Gets the builder collection for adding <see cref="IValidationType"/> implementations.
        /// </summary>
        /// <param name="builder">The Umbraco builder.</param>
        /// <returns>A <see cref="ValidationTypeCollectionBuilder"/>.</returns>
        public static ValidationTypeCollectionBuilder ValidationTypes(this IUmbracoBuilder builder)
        {
            return builder.WithCollectionBuilder<ValidationTypeCollectionBuilder>();
        }
    }
}
