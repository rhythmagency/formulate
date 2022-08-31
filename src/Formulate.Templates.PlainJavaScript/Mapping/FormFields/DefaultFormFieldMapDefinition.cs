namespace Formulate.Templates.PlainJavaScript.Mapping.FormFields
{
    using Formulate.Core.FormFields;
    using Formulate.Templates.PlainJavaScript;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
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
