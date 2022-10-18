namespace Formulate.Extensions.PlainJavaScriptTemplate.Core
{
    using Formulate.Extensions.PlainJavaScriptTemplate.Core.Mapping.FormFields;
    using Formulate.Extensions.PlainJavaScriptTemplate.Core.Mapping.Layouts;
    using Formulate.Extensions.PlainJavaScriptTemplate.Core.Mapping.Validations;
    using Umbraco.Cms.Core.DependencyInjection;

    public static class UmbracoBuilderExtensions
    {

        public static IUmbracoBuilder AddMapDefinitions(this IUmbracoBuilder umbracoBuilder)
        {
            umbracoBuilder.MapDefinitions().Add<DefaultFormFieldMapDefinition>();
            umbracoBuilder.MapDefinitions().Add<HeaderFieldMapDefinition>();
            umbracoBuilder.MapDefinitions().Add<DropDownFieldMapDefinition>();
            umbracoBuilder.MapDefinitions().Add<CheckboxListFieldMapDefinition>();
            umbracoBuilder.MapDefinitions().Add<RichTextFieldMapDefinition>();
            umbracoBuilder.MapDefinitions().Add<RadioButtonListFieldMapDefinition>();

            umbracoBuilder.MapDefinitions().Add<DefaultValidationMapDefinition>();
            umbracoBuilder.MapDefinitions().Add<MandatoryValidationMapDefinition>();
            umbracoBuilder.MapDefinitions().Add<RegexValidationMapDefinition>();

            umbracoBuilder.MapDefinitions().Add<DefaultLayoutMapDefinition>();
            umbracoBuilder.MapDefinitions().Add<BasicLayoutMapDefinition>();

            return umbracoBuilder;
        }
    }
}
