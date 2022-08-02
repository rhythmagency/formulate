namespace Formulate.BackOffice.Mapping.EditorModels
{
    using Formulate.BackOffice.EditorModels;
    using Formulate.Core.Persistence;
    using Umbraco.Cms.Core.Mapping;

    public abstract class EditorModelMapDefinition<TPersistedEntity, TEditorModel> : IMapDefinition 
        where TPersistedEntity : IPersistedEntity
        where TEditorModel : IEditorModel
    {
        public void DefineMaps(IUmbracoMapper mapper)
        {
            mapper.Define<TPersistedEntity, IEditorModel>((block, mapperContext) => Map(block, mapperContext));
        }

        protected virtual TEditorModel Map(TPersistedEntity entity, MapperContext mapperContext)
        {
            if (mapperContext.Items.TryGetValue("isNew", out var value) && value is bool isNew)
            {
                return Map(entity, isNew);
            }

            return Map(entity, false);
        }


        protected abstract TEditorModel Map(TPersistedEntity entity, bool isNew);
    }
}
