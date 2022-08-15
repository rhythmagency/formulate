namespace Formulate.BackOffice.Utilities
{
    using Formulate.BackOffice.EditorModels;
    using Formulate.Core.Persistence;
    using Umbraco.Cms.Core.Mapping;

    internal sealed class MapEditorModel : IMapEditorModel
    {
        private readonly IUmbracoMapper _umbracoMapper;

        public MapEditorModel(IUmbracoMapper umbracoMapper)
        {
            _umbracoMapper = umbracoMapper;
        }

        public IEditorModel? MapTo(MapToEditorModelInput input)
        {
            if (input.Entity is null)
            {
                return default;
            }

            return _umbracoMapper.Map<IEditorModel>(input.Entity, (context) => { context.Items.Add("isNew", input.IsNew); });
        }

        public TEntity? MapFrom<TEditorModel, TEntity>(TEditorModel input)
            where TEditorModel : IEditorModel
            where TEntity : IPersistedEntity
        {
            if (input is null)
            {
                return default;
            }

            return _umbracoMapper.Map<TEditorModel, TEntity>(input);
        }
    }
}
