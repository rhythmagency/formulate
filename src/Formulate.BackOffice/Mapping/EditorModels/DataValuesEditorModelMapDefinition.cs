namespace Formulate.BackOffice.Mapping.EditorModels
{
    using Formulate.BackOffice.EditorModels.DataValues;
    using Formulate.Core.DataValues;
    using Formulate.Core.Types;
    using System.Linq;

    internal sealed class DataValuesEditorModelMapDefinition : EditorModelMapDefinition<PersistedDataValues, DataValuesEditorModel>
    {
        private readonly DataValuesDefinitionCollection _dataValuesDefinitions;

        public DataValuesEditorModelMapDefinition(DataValuesDefinitionCollection dataValuesDefinitions)
        {
            _dataValuesDefinitions = dataValuesDefinitions;
        }

        protected override DataValuesEditorModel? Map(PersistedDataValues entity, bool isNew)
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
    }


}
