namespace Formulate.Extensions.PlainJavaScriptTemplate.Core.Mapping.Validations
{
    using Formulate.Core.Validations.Mandatory;
    using Umbraco.Cms.Core.Mapping;

    public sealed class MandatoryValidationMapDefinition : ValidationMapDefinition<MandatoryValidation>
    {
        protected override PlainJavaScriptValidation Map(MandatoryValidation validation, MapperContext context)
        {
            var config = new
            {
                message = validation.Configuration.Message
            };

            return new PlainJavaScriptValidation(config, "required");
        }
    }
}
