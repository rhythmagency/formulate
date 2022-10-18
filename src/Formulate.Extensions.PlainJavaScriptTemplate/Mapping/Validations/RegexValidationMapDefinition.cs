namespace Formulate.Extensions.PlainJavaScriptTemplate.Mapping.Validations
{
    using Formulate.Core.Validations.Regex;
    using Umbraco.Cms.Core.Mapping;

    public sealed class RegexValidationMapDefinition : ValidationMapDefinition<RegexValidation>
    {
        protected override PlainJavaScriptValidation Map(RegexValidation validation, MapperContext context)
        {
            var validationConfig = validation.Configuration;
            var config = new
            {
                message = validationConfig.Message,
                pattern = validationConfig.Regex
            };

            return new PlainJavaScriptValidation(config, "regex");
        }
    }
}
