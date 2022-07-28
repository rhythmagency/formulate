namespace Formulate.Website.RenderModels
{
    using Formulate.Core.FormFields;

    public sealed class FormFieldRenderModel
    {
        public FormFieldRenderModel(IFormFieldDefinition definition, IFormField field)
        {
            Definition = definition;
            Field = field;
        }

        public IFormFieldDefinition Definition { get; init; }

        public IFormField Field { get; init; }
    }
}