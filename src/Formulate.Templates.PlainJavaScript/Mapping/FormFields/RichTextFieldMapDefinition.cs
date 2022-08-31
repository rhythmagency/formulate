namespace Formulate.Templates.PlainJavaScript.Mapping.FormFields
{
    using Formulate.Core.FormFields.RichText;
    using Formulate.Templates.PlainJavaScript;
    using Umbraco.Cms.Core.Mapping;

    public sealed class RichTextFieldMapDefinition : FormFieldMapDefinition<RichTextField>
    {
        protected override PlainJavaScriptFormField Map(RichTextField field, MapperContext context)
        {
            var fieldConfig = field.Configuration;
            var config = new
            {
                text = fieldConfig.Text
            };

            return new PlainJavaScriptFormField(config, "rich-text");
        }
    }
}
