namespace Formulate.BackOffice.Mapping.EditorModels
{
    using Formulate.BackOffice.EditorModels.Forms;
    using Formulate.Core.FormFields;
    using Formulate.Core.FormHandlers;
    using Formulate.Core.Forms;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Formulate.Core.Types;
    using Formulate.Core.Validations;
    using Umbraco.Cms.Core.Mapping;
    using Formulate.Core.Utilities;

    internal sealed class FormEditorModelMapDefinition : EditorModelMapDefinition<PersistedForm, FormEditorModel>
    {
        private readonly IJsonUtility _jsonUtility;

        private readonly FormFieldDefinitionCollection _formFieldDefinitions;

        private readonly FormHandlerDefinitionCollection _formHandlerDefinitions;

        private readonly IValidationEntityRepository _validationEntityRepository;

        public FormEditorModelMapDefinition(IJsonUtility jsonUtility, FormFieldDefinitionCollection formFieldDefinitions, FormHandlerDefinitionCollection formHandlerDefinitions,  IValidationEntityRepository validationEntityRepository)
        {
            _jsonUtility = jsonUtility;
            _formFieldDefinitions = formFieldDefinitions;
            _formHandlerDefinitions = formHandlerDefinitions;
            _validationEntityRepository = validationEntityRepository;
        }

        protected override FormEditorModel? MapToEditor(PersistedForm entity, bool isNew)
        {
            return new FormEditorModel(entity, isNew)
            {
                Fields = MapFields(entity.Fields),
                Handlers = MapHandlers(entity.Handlers)
            };
        }

        protected override PersistedForm? MapToEntity(FormEditorModel editorModel, MapperContext mapperContext)
        {
            return new PersistedForm()
            {
                Alias = editorModel.Alias,
                Id = editorModel.Id,
                Name = editorModel.Name,
                Path = editorModel.Path,
                Fields = MapFields(editorModel.Fields),
                Handlers = MapHandlers(editorModel.Handlers)
            };
        }


        private PersistedFormField[] MapFields(FormFieldEditorModel[] fields)
        {
            if (fields.Length == 0)
            {
                return Array.Empty<PersistedFormField>();
            }

            var mappedFields = new List<PersistedFormField>();

            foreach (var field in fields)
            {
                mappedFields.Add(new PersistedFormField()
                {
                    Alias = field.Alias,
                    Category = field.Category,
                    Id = field.Id,
                    KindId = field.KindId,
                    Label = field.Label,
                    Name = field.Name,
                    Validations = MapValidations(field.Validations),
                    Data = _jsonUtility.Serialize(field.Configuration)
                });
            }

            return mappedFields.ToArray();
        }

        private FormFieldEditorModel[] MapFields(PersistedFormField[] fields)
        {
            if (fields.Length == 0)
            {
                return Array.Empty<FormFieldEditorModel>();
            }

            var mappedFields = new List<FormFieldEditorModel>();

            foreach (var field in fields)
            {
                var definition = _formFieldDefinitions.FirstOrDefault(field.KindId);
                if (definition is null)
                {
                    continue;
                }

                var mappedField = new FormFieldEditorModel()
                {
                    Id = field.Id,
                    Label = field.Label,
                    Name = field.Name,
                    Alias = field.Alias,
                    KindId = field.KindId,
                    Configuration = definition.GetBackOfficeConfiguration(field),
                    SupportsValidation = definition.SupportsValidation,
                    Icon = definition.Icon,
                    Directive = definition.Directive,
                };

                if (definition.SupportsValidation)
                {
                    mappedField.Validations = MapValidations(field.Validations);
                }

                mappedFields.Add(mappedField);
            }

            return mappedFields.ToArray();
        }
        
        private FormHandlerEditorModel[] MapHandlers(PersistedFormHandler[] handlers)
        {
            if (handlers.Length == 0)
            {
                return Array.Empty<FormHandlerEditorModel>();
            }

            var mappedHandlers = new List<FormHandlerEditorModel>();

            foreach (var handler in handlers)
            {
                var definition = _formHandlerDefinitions.FirstOrDefault(handler.KindId);

                if (definition is null)
                {
                    continue;
                }

                var mappedHandler = new FormHandlerEditorModel()
                {
                    Alias = handler.Alias,
                    Directive = definition.Directive,
                    Configuration = definition.GetBackOfficeConfiguration(handler),
                    Enabled = handler.Enabled,
                    Icon = definition.Icon,
                    Id = handler.Id,
                    KindId = handler.KindId,
                    Name = handler.Name
                };

                mappedHandlers.Add(mappedHandler);
            }

            return mappedHandlers.ToArray();
        }

        private PersistedFormHandler[] MapHandlers(FormHandlerEditorModel[] handlers)
        {
            if (handlers is null || handlers.Any() == false)
            {
                return Array.Empty<PersistedFormHandler>();
            }

            var mappedHandlers = new List<PersistedFormHandler>();

            foreach (var handler in handlers)
            {
                mappedHandlers.Add(new PersistedFormHandler()
                {
                    Alias = handler.Alias,
                    Enabled = handler.Enabled,
                    Id = handler.Id,
                    KindId = handler.KindId,
                    Name = handler.Name,
                    Data = _jsonUtility.Serialize(handler.Configuration)
                });
            }

            return mappedHandlers.ToArray();
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
