namespace Formulate.Web.Utilities
{
    using Formulate.Core;
    using Formulate.Core.FormFields;
    using Formulate.Core.Forms;
    using Formulate.Core.Layouts;
    using Formulate.Core.RenderModels;
    using Formulate.Web.RenderModels;

    public sealed class BuildFormLayoutRenderModel : IBuildFormLayoutRenderModel
    {
        private readonly IFormEntityRepository _formEntityRepository;
        private readonly IFormFieldFactory _formFieldFactory;
        private readonly FormFieldDefinitionCollection _formFieldDefinitions;

        private readonly ILayoutEntityRepository _layoutEntityRepository;
        private readonly ILayoutFactory _layoutFactory;

        public BuildFormLayoutRenderModel(IFormEntityRepository formEntityRepository, IFormFieldFactory formFieldFactory, FormFieldDefinitionCollection formFieldDefinitions, ILayoutEntityRepository layoutEntityRepository, ILayoutFactory layoutFactory)
        {
            _formEntityRepository = formEntityRepository;
            _formFieldFactory = formFieldFactory;
            _formFieldDefinitions = formFieldDefinitions;

            _layoutEntityRepository = layoutEntityRepository;
            _layoutFactory = layoutFactory;
        }

        public FormLayoutRenderModel? Build(FormLayout formLayout)
        {
            var form = BuildForm(formLayout.FormId);
            var layout = BuildLayout(formLayout.LayoutId);
            
            if (form is null || layout is null)
            {
                return default;
            }

            return new FormLayoutRenderModel(form, layout);
        }

        private ILayout? BuildLayout(Guid layoutId)
        {
            var layout = _layoutEntityRepository.Get(layoutId);

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
