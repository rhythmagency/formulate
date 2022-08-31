namespace Formulate.Templates.PlainJavaScript.Mapping.FormFields
{
    using Formulate.Core.FormFields;
    using Formulate.Templates.PlainJavaScript;
    using Umbraco.Cms.Core.Mapping;

    public sealed class DefaultFormFieldMapDefinition : FormFieldMapDefinition<IFormField>
    {
        protected override PlainJavaScriptFormField Map(IFormField field, MapperContext context)
        {
            var fieldType = field.GetType().Name.ToLower().Replace("field", string.Empty);
            return new PlainJavaScriptFormField(fieldType);
        }
    }
}
