namespace Formulate.Extensions.PlainJavaScriptTemplate.Core.Mapping.FormFields
{
    using Formulate.Core.FormFields;
    using Umbraco.Cms.Core.Mapping;

    public abstract class FormFieldMapDefinition<TFormField> : IMapDefinition where TFormField : IFormField
    {
        public void DefineMaps(IUmbracoMapper mapper)
        {
            mapper.Define<TFormField, PlainJavaScriptFormField>((x, context) => Map(x, context));
        }

        protected abstract PlainJavaScriptFormField Map(TFormField field, MapperContext context);
    }
}
