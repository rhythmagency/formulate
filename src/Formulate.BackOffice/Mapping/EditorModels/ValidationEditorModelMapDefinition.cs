namespace Formulate.BackOffice.Mapping.EditorModels
{
    using Formulate.BackOffice.EditorModels.Validation;
    using Formulate.Core.Types;
    using Formulate.Core.Validations;

    internal sealed class ValidationEditorModelMapDefinition : EditorModelMapDefinition<PersistedValidation, ValidationEditorModel>
    {
        private readonly ValidationDefinitionCollection _validationDefinitions;

        public ValidationEditorModelMapDefinition(ValidationDefinitionCollection validationDefinitions)
        {
            _validationDefinitions = validationDefinitions;
        }

        protected override ValidationEditorModel? Map(PersistedValidation entity, bool isNew)
        {
            var definition = _validationDefinitions.FirstOrDefault(entity.KindId);

            if (definition is null)
            {
                return default;
            }

            return new ValidationEditorModel(entity, isNew)
            {
                Data = definition.GetBackOfficeConfiguration(entity),
                Directive = definition.Directive
            };
        }
    }
}
