namespace Formulate.BackOffice.Mapping.EditorModels
{
    using Formulate.BackOffice.EditorModels.Layouts;
    using Formulate.Core.Layouts;
    using Formulate.Core.Types;
    using Formulate.Core.Utilities;
    using Umbraco.Cms.Core.Mapping;

    internal sealed class LayoutEditorModelMapDefinition : EntityEditorModelMapDefinition<PersistedLayout, LayoutEditorModel>
    {
        private readonly IJsonUtility _jsonUtility;

        private readonly LayoutDefinitionCollection _layoutDefinitions;

        public LayoutEditorModelMapDefinition(IJsonUtility jsonUtility, LayoutDefinitionCollection layoutDefinitions)
        {
            _jsonUtility = jsonUtility;
            _layoutDefinitions = layoutDefinitions;
        }

        public override LayoutEditorModel? MapToEditor(PersistedLayout entity, MapperContext mapperContext)
        {
            var definition = _layoutDefinitions.FirstOrDefault(entity.KindId);

            if (definition is null)
            {
                return default;
            }

            return new LayoutEditorModel(entity, mapperContext.IsNew())
            {
                Directive = definition.Directive,
                Data = definition.GetBackOfficeConfiguration(entity)
            };
        }

        public override PersistedLayout? MapToEntity(LayoutEditorModel editorModel, MapperContext mapperContext)
        {
            return new PersistedLayout()
            {
                Id = editorModel.Id,
                Alias = editorModel.Alias,
                KindId = editorModel.KindId,
                Name = editorModel.Name,
                Path = editorModel.Path,
                Data = _jsonUtility.Serialize(editorModel.Data)
            };
        }
    }
}
