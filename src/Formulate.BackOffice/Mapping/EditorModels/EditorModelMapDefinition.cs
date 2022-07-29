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

        protected abstract TEditorModel Map(TPersistedEntity entity, MapperContext mapperContext);
    }
}
