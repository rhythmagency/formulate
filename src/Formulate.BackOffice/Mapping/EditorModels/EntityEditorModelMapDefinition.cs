namespace Formulate.BackOffice.Mapping.EditorModels
{
    using Formulate.BackOffice.EditorModels;
    using Formulate.Core.Persistence;
    using Umbraco.Cms.Core.Mapping;

    public abstract class EntityEditorModelMapDefinition<TPersistedEntity, TEditorModel> : IMapDefinition
        where TPersistedEntity : IPersistedEntity
        where TEditorModel : IEditorModel
    {
        public void DefineMaps(IUmbracoMapper mapper)
        {
            mapper.Define<TPersistedEntity, IEditorModel?>((entity, mapperContext) => MapToEditor(entity, mapperContext));

            mapper.Define<TEditorModel, TPersistedEntity?>((editorModel, mapperContext) => MapToEntity(editorModel, mapperContext));
        }

        public abstract TPersistedEntity? MapToEntity(TEditorModel editorModel, MapperContext mapperContext);

        public abstract TEditorModel? MapToEditor(TPersistedEntity entity, MapperContext mapperContext);
    }
}
