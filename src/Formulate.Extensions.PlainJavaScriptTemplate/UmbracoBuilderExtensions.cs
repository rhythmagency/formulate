namespace Formulate.Extensions.PlainJavaScriptTemplate
{
    using Formulate.Extensions.PlainJavaScriptTemplate.Mapping.FormFields;
    using Formulate.Extensions.PlainJavaScriptTemplate.Mapping.Layouts;
    using Formulate.Extensions.PlainJavaScriptTemplate.Mapping.Validations;
    using Umbraco.Cms.Core.DependencyInjection;

    internal static class UmbracoBuilderExtensions
    {
        public static IUmbracoBuilder AddPackageManifest(this IUmbracoBuilder umbracoBuilder)
        {
            if (umbracoBuilder.ManifestFilters().Has<PackageManifestFilter>())
            {
                return umbracoBuilder;
            }

            umbracoBuilder.ManifestFilters().Append<PackageManifestFilter>();

            return umbracoBuilder;
        }

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
