namespace Formulate.Templates.PlainJavaScript.Mapping.Validations
{
    using Formulate.Core.Validations;
    using Formulate.Core.Validations.Regex;
    using Umbraco.Cms.Core.Mapping;

    public sealed class DefaultValidationMapDefinition : ValidationMapDefinition<IValidation>
    {
        protected override PlainJavaScriptValidation Map(IValidation validation, MapperContext context)
        {
            var validationType = validation.GetType().Name.ToLower().Replace("validation", string.Empty);

            return new PlainJavaScriptValidation(validationType);
        }
    }
}
