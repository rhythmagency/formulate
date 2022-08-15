namespace Formulate.BackOffice.Mapping.EditorModels
{
    using Formulate.BackOffice.EditorModels.DataValues;
    using Formulate.Core.DataValues;
    using Formulate.Core.Types;
    using Formulate.Core.Utilities;
    using System.Linq;
    using Umbraco.Cms.Core.Mapping;

    internal sealed class DataValuesEditorModelMapDefinition : EditorModelMapDefinition<PersistedDataValues, DataValuesEditorModel>
    {
        private readonly IJsonUtility _jsonUtility;

        private readonly DataValuesDefinitionCollection _dataValuesDefinitions;

        public DataValuesEditorModelMapDefinition(IJsonUtility jsonUtility, DataValuesDefinitionCollection dataValuesDefinitions)
        {
            _jsonUtility = jsonUtility;
            _dataValuesDefinitions = dataValuesDefinitions;
        }

        protected override DataValuesEditorModel? MapToEditor(PersistedDataValues entity, bool isNew)
        {
            var definition = _dataValuesDefinitions.FirstOrDefault(entity.KindId);
            
            if (definition is null)
            {
                return default;
            }

            return new DataValuesEditorModel(entity, isNew)
            {
                Directive = definition.Directive,
                Data = definition.GetBackOfficeConfiguration(entity),
                IsLegacy = definition.IsLegacy,
            };
        }

        protected override PersistedDataValues? MapToEntity(DataValuesEditorModel editorModel, MapperContext mapperContext)
        {
            return new PersistedDataValues()
            {
                Alias = editorModel.Alias,
                Id = editorModel.Id,
                KindId = editorModel.KindId,
                Name = editorModel.Name,
                Path = editorModel.Path,
                Data = _jsonUtility.Serialize(editorModel.Data)
            };
        }
    }


}
