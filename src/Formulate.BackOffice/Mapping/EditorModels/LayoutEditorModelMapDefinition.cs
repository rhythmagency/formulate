namespace Formulate.BackOffice.Mapping.EditorModels
{
    using Formulate.BackOffice.EditorModels.Layouts;
    using Formulate.Core.Layouts;
    using Umbraco.Cms.Core.Mapping;

    internal sealed class LayoutEditorModelMapDefinition : EditorModelMapDefinition<PersistedLayout, LayoutEditorModel>
    {
        protected override LayoutEditorModel Map(PersistedLayout entity, MapperContext mapperContext)
        {
            return new LayoutEditorModel(entity);
        }
    }
}
