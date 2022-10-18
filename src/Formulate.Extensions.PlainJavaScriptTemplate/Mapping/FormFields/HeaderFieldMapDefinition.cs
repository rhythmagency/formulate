namespace Formulate.Extensions.PlainJavaScriptTemplate.Mapping.FormFields
{
    using Formulate.Core.FormFields.Header;
    using Formulate.Extensions.PlainJavaScriptTemplate;
    using Umbraco.Cms.Core.Mapping;

    public sealed class HeaderFieldMapDefinition : FormFieldMapDefinition<HeaderField>
    {
        protected override PlainJavaScriptFormField Map(HeaderField field, MapperContext context)
        {
            var fieldConfig = field.Configuration;
            var config = new
            {
                text = fieldConfig.Text
            };

            return new PlainJavaScriptFormField(config, "header");
        }
    }
}
