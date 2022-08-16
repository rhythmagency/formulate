namespace Formulate.BackOffice.Mapping.EditorModels
{
    using Formulate.BackOffice.EditorModels.Forms;
    using Formulate.Core.FormFields;
    using Formulate.Core.FormHandlers;
    using Formulate.Core.Forms;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Umbraco.Cms.Core.Mapping;

    internal sealed class FormEditorModelMapDefinition : EntityEditorModelMapDefinition<PersistedForm, FormEditorModel>
    {
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
                Handlers = MapHandlers(editorModel.Handlers, mapperContext)
            };
        }

        private static PersistedFormField[] MapFields(FormFieldEditorModel[] fields, MapperContext mapperContext)
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

        private static FormFieldEditorModel[] MapFields(PersistedFormField[] fields, MapperContext mapperContext)
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
        
        private static FormHandlerEditorModel[] MapHandlers(PersistedFormHandler[] handlers, MapperContext mapperContext)
        {
            if (handlers.Length == 0)
            {
                return Array.Empty<FormHandlerEditorModel>();
            }

            var mappedHandlers = new List<FormHandlerEditorModel>();

            foreach (var handler in handlers)
            {
                var mappedHandler = mapperContext.Map<PersistedFormHandler, FormHandlerEditorModel>(handler);

                if (mappedHandler is not null)
                {
                    mappedHandlers.Add(mappedHandler);
                }
            }

            return mappedHandlers.ToArray();
        }

        private static PersistedFormHandler[] MapHandlers(FormHandlerEditorModel[] handlers, MapperContext mapperContext)
        {
            if (handlers is null || handlers.Any() == false)
            {
                return Array.Empty<PersistedFormHandler>();
            }

            var mappedHandlers = new List<PersistedFormHandler>();

            foreach (var handler in handlers)
            {
                var mappedHandler = mapperContext.Map<FormHandlerEditorModel, PersistedFormHandler>(handler);

                if (mappedHandler is not null)
                {
                    mappedHandlers.Add(mappedHandler);
                }
            }

            return mappedHandlers.ToArray();
        }
    }
}
