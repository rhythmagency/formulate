namespace Formulate.BackOffice.DependencyInjection
{
    using Formulate.BackOffice.Mapping.EditorModels;
    using Umbraco.Cms.Core.DependencyInjection;

    public static partial class UmbracoBuilderExtensions
    {
        /// <summary>
        /// Adds Formulate Map Definitions.
        /// </summary>
        /// <param name="builder">
        /// The Umbraco builder.
        /// </param>
        /// <returns>
        /// The current <see cref="IUmbracoBuilder"/>.
        /// </returns>
        private static IUmbracoBuilder AddFormulateMapDefinitions(
            this IUmbracoBuilder builder)
        {
            builder.MapDefinitions().Add<DataValuesEditorModelMapDefinition>();
            builder.MapDefinitions().Add<ConfiguredFormEditorModelMapDefinition>();
            builder.MapDefinitions().Add<FolderEditorModelMapDefinition>();
            builder.MapDefinitions().Add<FormFieldEditorModelMapDefinition>();
            builder.MapDefinitions().Add<FormHandlerEditorModelMapDefinition>();
            builder.MapDefinitions().Add<FormEditorModelMapDefinition>();
            builder.MapDefinitions().Add<LayoutEditorModelMapDefinition>();
            builder.MapDefinitions().Add<ValidationEditorModelMapDefinition>();

            return builder;
        }
    }
}
