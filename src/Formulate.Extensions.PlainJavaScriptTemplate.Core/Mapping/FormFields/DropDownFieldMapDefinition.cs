namespace Formulate.Extensions.PlainJavaScriptTemplate.Core.Mapping.FormFields
{
    using Formulate.Core.FormFields.DropDown;
    using Formulate.Extensions.PlainJavaScriptTemplate;
    using Umbraco.Cms.Core.Mapping;

    public sealed class DropDownFieldMapDefinition : FormFieldMapDefinition<DropDownField>
    {
        protected override PlainJavaScriptFormField Map(DropDownField field, MapperContext context)
        {
            var fieldConfig = field.Configuration;
            var config = new
            {
                items = fieldConfig.Items.Select(y => new
                {
                    value = y.Value,
                    label = y.Label,
                    selected = y.Selected
                }).ToArray()
            };

            return new PlainJavaScriptFormField(config, "select");
        }
    }

}
