namespace Formulate.Templates.PlainJavaScript.Mapping.FormFields
{
    using Formulate.Core.FormFields.CheckboxList;
    using Formulate.Templates.PlainJavaScript;
    using Umbraco.Cms.Core.Mapping;

    public sealed class CheckboxListFieldMapDefinition : FormFieldMapDefinition<CheckboxListField>
    {
        protected override PlainJavaScriptFormField Map(CheckboxListField field, MapperContext context)
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

            return new PlainJavaScriptFormField(config, "checkbox-list");
        }
    }

}
