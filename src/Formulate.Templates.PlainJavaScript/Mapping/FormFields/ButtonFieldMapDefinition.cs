﻿namespace Formulate.Templates.PlainJavaScript.Mapping.FormFields
{
    using Formulate.Core.FormFields.Button;
    using Formulate.Templates.PlainJavaScript;
    using Umbraco.Cms.Core.Mapping;

    public sealed class ButtonFieldMapDefinition : FormFieldMapDefinition<ButtonField>
    {
        protected override PlainJavaScriptFormField Map(ButtonField field, MapperContext context)
        {
            var fieldConfig = field.Configuration;
            var config = new
            {
                buttonKind = fieldConfig.ButtonKind
            };

            return new PlainJavaScriptFormField(config, "button");
        }
    }

}
