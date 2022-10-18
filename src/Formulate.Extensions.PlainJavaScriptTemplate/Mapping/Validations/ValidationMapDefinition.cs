namespace Formulate.Extensions.PlainJavaScriptTemplate.Mapping.Validations
{
    using Formulate.Core.Validations;
    using Umbraco.Cms.Core.Mapping;

    public abstract class ValidationMapDefinition<TValidation> : IMapDefinition where TValidation : IValidation
    {
        public void DefineMaps(IUmbracoMapper mapper)
        {
            mapper.Define<TValidation, PlainJavaScriptValidation>((x, context) => Map(x, context));
        }

        protected abstract PlainJavaScriptValidation Map(TValidation validation, MapperContext context);
    }
}
