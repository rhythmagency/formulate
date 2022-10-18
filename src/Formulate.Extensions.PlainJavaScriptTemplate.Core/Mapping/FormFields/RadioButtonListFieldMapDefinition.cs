namespace Formulate.Extensions.PlainJavaScriptTemplate.Core.Mapping.FormFields
{
    using Formulate.Core.FormFields.RadioButtonList;
    using Formulate.Extensions.PlainJavaScriptTemplate;
    using Umbraco.Cms.Core.Mapping;

    public sealed class RadioButtonListFieldMapDefinition : FormFieldMapDefinition<RadioButtonListField>
    {
        protected override PlainJavaScriptFormField Map(RadioButtonListField field, MapperContext context)
        {
            var fieldConfig = field.Configuration;
            var config = new
            {
                orientation = fieldConfig.Orientation,
                items = fieldConfig.Items.Select(y => new
                {
                    value = y.Value,
                    label = y.Label,
                    selected = y.Selected
                }).ToArray()
            };

            return new PlainJavaScriptFormField(config, "radio-list");
        }
    }

}
