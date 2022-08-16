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
    using Umbraco.Cms.Core.Mapping;
    using Formulate.Core.Utilities;

    internal sealed class FormEditorModelMapDefinition : EntityEditorModelMapDefinition<PersistedForm, FormEditorModel>
    {
        private readonly IJsonUtility _jsonUtility;

        private readonly FormHandlerDefinitionCollection _formHandlerDefinitions;

        public FormEditorModelMapDefinition(IJsonUtility jsonUtility, FormFieldDefinitionCollection formFieldDefinitions, FormHandlerDefinitionCollection formHandlerDefinitions)
        {
            _jsonUtility = jsonUtility;
            _formHandlerDefinitions = formHandlerDefinitions;
        }

        public override FormEditorModel? MapToEditor(PersistedForm entity, MapperContext mapperContext)
        {
            return new FormEditorModel(entity, mapperContext.IsNew())
            {
                Fields = MapFields(entity.Fields, mapperContext),
                Handlers = MapHandlers(entity.Handlers, mapperContext)
            };
        }

        public override PersistedForm? MapToEntity(FormEditorModel editorModel, MapperContext mapperContext)
        {
            return new PersistedForm()
            {
                Alias = editorModel.Alias,
                Id = editorModel.Id,
                Name = editorModel.Name,
                Path = editorModel.Path,
                Fields = MapFields(editorModel.Fields, mapperContext),
                Handlers = MapHandlers(editorModel.Handlers)
            };
        }


        private PersistedFormField[] MapFields(FormFieldEditorModel[] fields, MapperContext mapperContext)
        {
            if (fields.Length == 0)
            {
                return Array.Empty<PersistedFormField>();
            }

            var mappedFields = new List<PersistedFormField>();

            foreach (var field in fields)
            {
                var mappedField = mapperContext.Map<FormFieldEditorModel, PersistedFormField>(field);

                if (mappedField is not null)
                {
                    mappedFields.Add(mappedField);
                }
            }

            return mappedFields.ToArray();
        }

        private FormFieldEditorModel[] MapFields(PersistedFormField[] fields, MapperContext mapperContext)
        {
            if (fields.Length == 0)
            {
                return Array.Empty<FormFieldEditorModel>();
            }

            var mappedFields = new List<FormFieldEditorModel>();

            foreach (var field in fields)
            {
                var mappedField = mapperContext.Map<PersistedFormField, FormFieldEditorModel>(field);

                if (mappedField is not null)
                {
                    mappedFields.Add(mappedField);
                }

            }

            return mappedFields.ToArray();
        }
        
        private FormHandlerEditorModel[] MapHandlers(PersistedFormHandler[] handlers, MapperContext mapperContext)
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
    }
}
