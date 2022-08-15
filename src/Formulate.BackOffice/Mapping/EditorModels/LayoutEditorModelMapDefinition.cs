namespace Formulate.BackOffice.Mapping.EditorModels
{
    using Formulate.BackOffice.EditorModels.Layouts;
    using Formulate.Core.Layouts;
    using Formulate.Core.Types;
    using Formulate.Core.Utilities;
    using Umbraco.Cms.Core.Mapping;

    internal sealed class LayoutEditorModelMapDefinition : EditorModelMapDefinition<PersistedLayout, LayoutEditorModel>
    {
        private readonly IJsonUtility _jsonUtility;

        private readonly LayoutDefinitionCollection _layoutDefinitions;

        public LayoutEditorModelMapDefinition(IJsonUtility jsonUtility, LayoutDefinitionCollection layoutDefinitions)
        {
            _jsonUtility = jsonUtility;
            _layoutDefinitions = layoutDefinitions;
        }

        protected override LayoutEditorModel? Map(PersistedLayout entity, bool isNew)
        {
            var definition = _layoutDefinitions.FirstOrDefault(entity.KindId);

            if (definition is null)
            {
                return default;
            }

            return new LayoutEditorModel(entity, isNew)
            {
                Directive = definition.Directive,
                Data = definition.GetBackOfficeConfiguration(entity)
            };
        }

        protected override PersistedLayout? MapToEntity(LayoutEditorModel editorModel, MapperContext mapperContext)
        {
            return new PersistedLayout()
            {
                Id = editorModel.Id,
                KindId = editorModel.KindId,
                Name = editorModel.Name,
                Path = editorModel.Path,
                Data = _jsonUtility.Serialize(editorModel.Data)
            };
        }
    }
}
