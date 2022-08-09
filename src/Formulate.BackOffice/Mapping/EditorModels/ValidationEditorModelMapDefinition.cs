namespace Formulate.BackOffice.Mapping.EditorModels
{
    using Formulate.BackOffice.EditorModels.Validation;
    using Formulate.Core.Validations;

    internal sealed class ValidationEditorModelMapDefinition : EditorModelMapDefinition<PersistedValidation, ValidationEditorModel>
    {
        protected override ValidationEditorModel? Map(PersistedValidation entity, bool isNew)
        {
            return new ValidationEditorModel(entity, isNew);
        }
    }
}
