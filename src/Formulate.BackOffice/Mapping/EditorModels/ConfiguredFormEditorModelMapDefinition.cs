namespace Formulate.BackOffice.Mapping.EditorModels
{
    using Formulate.BackOffice.EditorModels.ConfiguredForm;
    using Formulate.Core.ConfiguredForms;
    using Formulate.Core.Layouts;
    using Formulate.Core.Templates;
    using System;
    using System.Linq;

    internal sealed class ConfiguredFormEditorModelMapDefinition : EditorModelMapDefinition<PersistedConfiguredForm, ConfiguredFormEditorModel>
    {
        private readonly TemplateDefinitionCollection _templateDefinitions;
        private readonly ILayoutEntityRepository _layoutEntities;

        public ConfiguredFormEditorModelMapDefinition(TemplateDefinitionCollection templateDefinitions, ILayoutEntityRepository layoutEntityRepository)
        {
            _templateDefinitions = templateDefinitions;
            _layoutEntities = layoutEntityRepository;
        }

        protected override ConfiguredFormEditorModel Map(PersistedConfiguredForm entity, bool isNew)
        {
            var layout = GetLayout(entity.LayoutId);
            var template = GetTemplate(entity.TemplateId);
            var editorModel = new ConfiguredFormEditorModel(entity, isNew);

            if (layout is not null)
            {
                editorModel.LayoutId = layout.Id;
                editorModel.LayoutName = layout.Name;
            }

            if (template is not null)
            {
                editorModel.TemplateId = template.Id;
                editorModel.TemplateName = template.Name;
            }

            return editorModel;
        }

        private ITemplateDefinition GetTemplate(Guid? templateId)
        {
            if (templateId is null || templateId.HasValue == false)
            {
                return default;
            }

            return _templateDefinitions.FirstOrDefault(x => x.Id == templateId);
        }

        private PersistedLayout GetLayout(Guid? layoutId)
        {
            if (layoutId is null || layoutId.HasValue == false)
            {
                return default;
            }

            return _layoutEntities.Get(layoutId.Value);
        }
    }
}
