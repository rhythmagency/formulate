namespace Formulate.BackOffice.Mapping.EditorModels
{
    using Formulate.BackOffice.EditorModels.DataValues;
    using Formulate.Core.DataValues;

    internal sealed class DataValuesEditorModelMapDefinition : EditorModelMapDefinition<PersistedDataValues, DataValuesEditorModel>
    {
        protected override DataValuesEditorModel Map(PersistedDataValues entity, bool isNew)
        {
            return new DataValuesEditorModel(entity, isNew);
        }
    }


}
