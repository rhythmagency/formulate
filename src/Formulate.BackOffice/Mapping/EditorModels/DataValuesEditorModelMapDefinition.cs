namespace Formulate.BackOffice.Mapping.EditorModels
{
    using Formulate.BackOffice.EditorModels.DataValues;
    using Formulate.Core.DataValues;
    using Umbraco.Cms.Core.Mapping;

    internal sealed class DataValuesEditorModelMapDefinition : EditorModelMapDefinition<PersistedDataValues, DataValuesEditorModel>
    {
        protected override DataValuesEditorModel Map(PersistedDataValues entity, MapperContext mapperContext)
        {
            return new DataValuesEditorModel(entity);
        }
    }


}
