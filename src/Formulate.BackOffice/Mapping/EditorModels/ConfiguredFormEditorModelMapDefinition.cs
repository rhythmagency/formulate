namespace Formulate.BackOffice.Mapping.EditorModels
{
    using Formulate.BackOffice.EditorModels.ConfiguredForm;
    using Formulate.Core.ConfiguredForms;
    using Formulate.Core.Layouts;
    using Formulate.Core.Templates;
    using Formulate.Core.Types;

    using System;
    using System.Linq;
    using Umbraco.Cms.Core.Mapping;

    internal sealed class ConfiguredFormEditorModelMapDefinition : EntityEditorModelMapDefinition<PersistedConfiguredForm, ConfiguredFormEditorModel>
    {
        private readonly TemplateDefinitionCollection _templateDefinitions;
        private readonly ILayoutEntityRepository _layoutEntities;

        public ConfiguredFormEditorModelMapDefinition(TemplateDefinitionCollection templateDefinitions, ILayoutEntityRepository layoutEntityRepository)
        {
            _templateDefinitions = templateDefinitions;
            _layoutEntities = layoutEntityRepository;
        }

        public override ConfiguredFormEditorModel? MapToEditor(PersistedConfiguredForm entity, MapperContext mapperContext)
        {
            var layout = GetLayout(entity.LayoutId);
            var template = GetTemplate(entity.TemplateId);
            var editorModel = new ConfiguredFormEditorModel(entity, mapperContext.IsNew());

            if (layout is not null)
            {
                editorModel.Layout = new ConfiguredFormLayoutEditorModel(layout);
            }

            if (template is not null)
            {
                editorModel.TemplateId = template.Id;
            }

            return editorModel;
        }

        private ITemplateDefinition? GetTemplate(Guid? templateId)
        {
            if (templateId is null || templateId.HasValue == false)
            {
                return default;
            }

            return _templateDefinitions.FirstOrDefault(x => x.Id == templateId);
        }

        private PersistedLayout? GetLayout(Guid? layoutId)
        {
            if (layoutId is null || layoutId.HasValue == false)
            {
                return default;
            }

            return _layoutEntities.Get(layoutId.Value);
        }

        public override PersistedConfiguredForm? MapToEntity(ConfiguredFormEditorModel editorModel, MapperContext mapperContext)
        {
            return new PersistedConfiguredForm()
            {
                Alias = editorModel.Alias,
                Id = editorModel.Id,
                LayoutId = editorModel.Layout?.Id,
                Name = editorModel.Name,
                Path = editorModel.Path,
                TemplateId = editorModel.TemplateId
            };
        }
    }
}
