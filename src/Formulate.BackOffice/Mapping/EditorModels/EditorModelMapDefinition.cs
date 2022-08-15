namespace Formulate.BackOffice.Mapping.EditorModels
{
    using Formulate.BackOffice.EditorModels;
    using Formulate.Core.Persistence;
    using Umbraco.Cms.Core.Mapping;

    public abstract class EditorModelMapDefinition<TPersistedEntity, TEditorModel> : IMapDefinition 
        where TPersistedEntity : IPersistedEntity
        where TEditorModel : IEditorModel?
    {
        public void DefineMaps(IUmbracoMapper mapper)
        {
            mapper.Define<TPersistedEntity, IEditorModel?>((entity, mapperContext) => MapFromEntity(entity, mapperContext));

            mapper.Define<TEditorModel, TPersistedEntity?>((editorModel, mapperContext) => MapToEntity(editorModel, mapperContext));
        }

        protected abstract TPersistedEntity? MapToEntity(TEditorModel editorModel, MapperContext mapperContext);

        protected virtual TEditorModel? MapFromEntity(TPersistedEntity entity, MapperContext mapperContext)
        {
            if (mapperContext.Items.TryGetValue("isNew", out var value) && value is bool isNew)
            {
                return Map(entity, isNew);
            }

            return Map(entity, false);
        }

        protected abstract TEditorModel? Map(TPersistedEntity entity, bool isNew);
    }
}
