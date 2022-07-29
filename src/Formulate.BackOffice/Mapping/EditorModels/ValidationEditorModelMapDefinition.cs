namespace Formulate.BackOffice.Mapping.EditorModels
{
    using Formulate.BackOffice.EditorModels.Validation;
    using Formulate.Core.Validations;
    using Umbraco.Cms.Core.Mapping;

    internal sealed class ValidationEditorModelMapDefinition : EditorModelMapDefinition<PersistedValidation, ValidationEditorModel>
    {
        protected override ValidationEditorModel Map(PersistedValidation entity, MapperContext mapperContext)
        {
            return new ValidationEditorModel(entity);
        }
    }
}
