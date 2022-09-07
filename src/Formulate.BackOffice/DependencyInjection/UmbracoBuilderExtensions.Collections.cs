namespace Formulate.BackOffice.DependencyInjection
{
    using Formulate.BackOffice.Definitions.Forms;
    using Umbraco.Cms.Core.DependencyInjection;

    public partial class UmbracoBuilderExtensions
    {
        private static void AddFormDefinitions(this IUmbracoBuilder builder)
        {
            builder.FormDefinitions().Add<DefaultFormLayoutDefinition>();
            builder.FormDefinitions().Add<FormWithBasicLayoutDefinition>();
        }

        /// <summary>
        /// Gets the builder collection for adding <see cref="IFormDefinition"/> implementations.
        /// </summary>
        /// <param name="builder">The Umbraco builder.</param>
        /// <returns>A <see cref="FormDefinitionCollectionBuilder"/>.</returns>
        public static FormDefinitionCollectionBuilder FormDefinitions(this IUmbracoBuilder builder)
        {
            return builder.WithCollectionBuilder<FormDefinitionCollectionBuilder>();
        }
    }
}
