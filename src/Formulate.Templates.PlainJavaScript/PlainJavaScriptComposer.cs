namespace Formulate.Templates.PlainJavaScript
{
    using Formulate.Core.DependencyInjection;
    using Formulate.Templates.PlainJavaScript.Mapping.FormFields;
    using Formulate.Templates.PlainJavaScript.Mapping.Layouts;
    using Formulate.Templates.PlainJavaScript.Mapping.Validations;
    using Microsoft.Extensions.DependencyInjection;
    using Umbraco.Cms.Core.Composing;
    using Umbraco.Cms.Core.DependencyInjection;

    public sealed class PlainJavaScriptComposer : IComposer
    {
        public void Compose(IUmbracoBuilder builder)
        {
            builder.TemplateDefinitions().Add<PlainJavaScriptTemplateDefinition>();
            builder.Services.AddSingleton<IBuildPlainJavaScriptJson, BuildPlainJavaScriptJson>();

            builder.MapDefinitions().Add<DefaultFormFieldMapDefinition>();
            builder.MapDefinitions().Add<HeaderFieldMapDefinition>();
            builder.MapDefinitions().Add<DropDownFieldMapDefinition>();
            builder.MapDefinitions().Add<CheckboxListFieldMapDefinition>();
            builder.MapDefinitions().Add<RichTextFieldMapDefinition>();
            builder.MapDefinitions().Add<RadioButtonListFieldMapDefinition>();

            builder.MapDefinitions().Add<DefaultValidationMapDefinition>();
            builder.MapDefinitions().Add<MandatoryValidationMapDefinition>();
            builder.MapDefinitions().Add<RegexValidationMapDefinition>();

            builder.MapDefinitions().Add<BasicLayoutMapDefinition>();

        }
    }
}
