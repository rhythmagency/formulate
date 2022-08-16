namespace Formulate.BackOffice.Mapping.EditorModels
{
    using Formulate.BackOffice.EditorModels;
    using Formulate.BackOffice.EditorModels.Forms;
    using Formulate.Core.FormHandlers;
    using Formulate.Core.Types;
    using Formulate.Core.Utilities;
    using System;
    using System.Linq;
    using Umbraco.Cms.Core.Mapping;

    public sealed class FormHandlerEditorModelMapDefinition : ItemEditorModelMapDefinition<PersistedFormHandler, FormHandlerEditorModel>
    {
        private readonly FormHandlerDefinitionCollection _formHandlerDefinitions;

        private readonly IJsonUtility _jsonUtility;

        public FormHandlerEditorModelMapDefinition(FormHandlerDefinitionCollection formHandlerDefinitions, IJsonUtility jsonUtility)
        {
            _formHandlerDefinitions = formHandlerDefinitions;
            _jsonUtility = jsonUtility;
        }

        public override FormHandlerEditorModel? MapToEditor(PersistedFormHandler entity, MapperContext mapperContext)
        {
            var definition = _formHandlerDefinitions.FirstOrDefault(entity.KindId);

            if (definition is null)
            {
                return default;
            }

            return new FormHandlerEditorModel()
            {
                Alias = entity.Alias,
                Directive = definition.Directive,
                Configuration = definition.GetBackOfficeConfiguration(entity),
                Enabled = entity.Enabled,
                Icon = definition.Icon,
                Id = entity.Id,
                KindId = entity.KindId,
                Name = entity.Name
            };
        }

        public override PersistedFormHandler? MapToItem(FormHandlerEditorModel editorModel, MapperContext mapperContext)
        {
            return new PersistedFormHandler()
            {
                Alias = editorModel.Alias,
                Enabled = editorModel.Enabled,
                Id = editorModel.Id,
                KindId = editorModel.KindId,
                Name = editorModel.Name,
                Data = _jsonUtility.Serialize(editorModel.Configuration)
            };
        }
    }
}
