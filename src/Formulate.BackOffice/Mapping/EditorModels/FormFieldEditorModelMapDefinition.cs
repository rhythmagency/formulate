namespace Formulate.BackOffice.Mapping.EditorModels
{
    using Formulate.BackOffice.EditorModels.Forms;
    using Formulate.Core.FormFields;
    using Formulate.Core.Types;
    using Formulate.Core.Utilities;
    using Formulate.Core.Validations;
    using System;
    using System.Linq;
    using Umbraco.Cms.Core.Mapping;

    public sealed class FormFieldEditorModelMapDefinition : ItemEditorModelMapDefinition<PersistedFormField, FormFieldEditorModel>
    {
        private readonly FormFieldDefinitionCollection _formFieldDefinitions;

        private readonly IJsonUtility _jsonUtility;

        private readonly IValidationEntityRepository _validationEntityRepository;

        public FormFieldEditorModelMapDefinition(FormFieldDefinitionCollection formFieldDefinitions, IJsonUtility jsonUtility, IValidationEntityRepository validationEntityRepository)
        {
            _formFieldDefinitions = formFieldDefinitions;
            _jsonUtility = jsonUtility;
            _validationEntityRepository = validationEntityRepository;
        }

        public override FormFieldEditorModel? MapToEditor(PersistedFormField entity, MapperContext mapperContext)
        {
            var definition = _formFieldDefinitions.FirstOrDefault(entity.KindId);
            if (definition is null)
            {
                return default;
            }

            var editorModel = new FormFieldEditorModel()
            {
                IsNew = mapperContext.IsNew(),
                Id = entity.Id,
                Name = entity.Name,
                Alias = entity.Alias,
                KindId = entity.KindId,
                Configuration = definition.GetBackOfficeConfiguration(entity),
                SupportsValidation = definition.SupportsValidation,
                SupportsLabel = definition.SupportsLabel,
                SupportsCategory = definition.SupportsCategory,
                Icon = definition.Icon,
                Directive = definition.Directive,
            };

            if (definition.SupportsCategory)
            {
                editorModel.Category = entity.Category;
            }

            if (definition.SupportsLabel)
            {
                editorModel.Label = entity.Label;
            }

            if (definition.SupportsValidation)
            {
                editorModel.Validations = MapValidations(entity.Validations);
            }

            return editorModel;
        }

        public override PersistedFormField? MapToItem(FormFieldEditorModel editorModel, MapperContext mapperContext)
        {
            return new PersistedFormField()
            {
                Alias = editorModel.Alias,
                Category = editorModel.Category,
                Id = editorModel.Id,
                KindId = editorModel.KindId,
                Label = editorModel.Label,
                Name = editorModel.Name,
                Validations = MapValidations(editorModel.Validations),
                Data = _jsonUtility.Serialize(editorModel.Configuration)
            };
        }

        private FormFieldValidationEditorModel[] MapValidations(Guid[] validations)
        {
            if (validations is null || validations.Any() == false)
            {
                return Array.Empty<FormFieldValidationEditorModel>();
            }

            // TODO: Improve this logic by including GetMultiple in repo.
            var entities = validations.Select(_validationEntityRepository.Get).Where(x => x is not null).ToArray();

            return entities.Select(x => new FormFieldValidationEditorModel()
            {
                Id = x.Id,
                Name = x.Name
            }).ToArray();
        }

        private Guid[] MapValidations(FormFieldValidationEditorModel[] validations)
        {
            if (validations.Length == 0)
            {
                return Array.Empty<Guid>();
            }

            return validations.Select(x => x.Id).ToArray();
        }
    }
}
