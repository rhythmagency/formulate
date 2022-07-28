namespace Formulate.Website.Utilities
{
    using Formulate.Core.ConfiguredForms;
    using Formulate.Core.FormFields;
    using Formulate.Core.Forms;
    using Formulate.Core.Layouts;
    using Formulate.Core.RenderModels;
    using Formulate.Website.RenderModels;

    public sealed class BuildConfiguredFormRenderModel : IBuildConfiguredFormRenderModel
    {
        private readonly IFormEntityRepository _formEntityRepository;
        private readonly IFormFieldFactory _formFieldFactory;
        private readonly FormFieldDefinitionCollection _formFieldDefinitions;

        private readonly ILayoutEntityRepository _layoutEntityRepository;
        private readonly ILayoutFactory _layoutFactory;

        public BuildConfiguredFormRenderModel(IFormEntityRepository formEntityRepository, IFormFieldFactory formFieldFactory, FormFieldDefinitionCollection formFieldDefinitions, ILayoutEntityRepository layoutEntityRepository, ILayoutFactory layoutFactory)
        {
            _formEntityRepository = formEntityRepository;
            _formFieldFactory = formFieldFactory;
            _formFieldDefinitions = formFieldDefinitions;

            _layoutEntityRepository = layoutEntityRepository;
            _layoutFactory = layoutFactory;
        }

        public ConfiguredFormRenderModel? Build(ConfiguredForm configuredForm)
        {
            var form = BuildForm(configuredForm.FormId);
            var layout = BuildLayout(configuredForm.LayoutId);
            
            if (form is null || layout is null)
            {
                return default;
            }

            return new ConfiguredFormRenderModel(form, layout);
        }

        private ILayout? BuildLayout(Guid? layoutId)
        {
            if (layoutId.HasValue == false)
            {
                return default;
            }

            var layout = _layoutEntityRepository.Get(layoutId.Value);

            if (layout is null)
            {
                return default;
            }

            return _layoutFactory.Create(layout);
        }

        public FormRenderModel? BuildForm(Guid formId)
        {
            var form = _formEntityRepository.Get(formId);

            if (form is null)
            {
                return default;
            }

            var formFieldRenderModels = new List<FormFieldRenderModel>();

            foreach (var field in form.Fields)
            {
                var createdField = _formFieldFactory.Create(field);

                if (createdField is null)
                {
                    // LOG: log this form field was not implemented as there was no definition.
                    continue;
                }

                var fieldDefinition = _formFieldDefinitions.FirstOrDefault(x => x.KindId == createdField.KindId);

                if (fieldDefinition is null)
                {
                    // LOG: log this form field was not implemented as there was no definition.
                    continue;
                }

                formFieldRenderModels.Add(new FormFieldRenderModel(fieldDefinition, createdField));
            }

            if (formFieldRenderModels.Any() == false)
            {
                return default;
            }

            return new FormRenderModel(form.Id, form.Name, form.Alias)
            {
                Fields = formFieldRenderModels
            };
        }
    }
}
