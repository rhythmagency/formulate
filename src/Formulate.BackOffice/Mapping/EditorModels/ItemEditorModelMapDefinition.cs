namespace Formulate.BackOffice.Mapping.EditorModels
{
    using Formulate.Core.Persistence;
    using Umbraco.Cms.Core.Mapping;

    public abstract class ItemEditorModelMapDefinition<TPersistedItem, TEditorModel> : IMapDefinition
        where TPersistedItem : IPersistedItem
        where TEditorModel : class        
    {
        public void DefineMaps(IUmbracoMapper mapper)
        {
            mapper.Define<TPersistedItem, TEditorModel?>((entity, mapperContext) => MapToEditor(entity, mapperContext));

            mapper.Define<TEditorModel, TPersistedItem?>((editorModel, mapperContext) => MapToItem(editorModel, mapperContext));
        }

        public abstract TEditorModel? MapToEditor(TPersistedItem entity, MapperContext mapperContext);

        public abstract TPersistedItem? MapToItem(TEditorModel editorModel, MapperContext mapperContext);
    }
}