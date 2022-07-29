namespace Formulate.BackOffice.Utilities
{
    using Formulate.BackOffice.EditorModels;
    using Formulate.Core.Persistence;
    using Umbraco.Cms.Core.Mapping;

    internal sealed class BuildEditorModel : IBuildEditorModel
    {
        private readonly IUmbracoMapper _umbracoMapper;

        public BuildEditorModel(IUmbracoMapper umbracoMapper)
        {
            _umbracoMapper = umbracoMapper;
        }

        public IEditorModel Build(IPersistedEntity entity)
        {
            return _umbracoMapper.Map<IEditorModel>(entity);
        }
    }
}
