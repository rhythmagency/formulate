namespace Formulate.BackOffice.Utilities
{
    using Formulate.BackOffice.EditorModels;
    using Umbraco.Cms.Core.Mapping;

    internal sealed class BuildEditorModel : IBuildEditorModel
    {
        private readonly IUmbracoMapper _umbracoMapper;

        public BuildEditorModel(IUmbracoMapper umbracoMapper)
        {
            _umbracoMapper = umbracoMapper;
        }

        public IEditorModel? Build(BuildEditorModelInput input)
        {
            if (input.Entity is null)
            {
                return default;
            }

            return _umbracoMapper.Map<IEditorModel>(input.Entity, (context) => { context.Items.Add("isNew", input.IsNew); });
        }
    }
}
