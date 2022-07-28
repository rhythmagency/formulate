namespace Formulate.Templates.PlainJavaScript
{
    using Formulate.Core.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection;
    using Umbraco.Cms.Core.Composing;
    using Umbraco.Cms.Core.DependencyInjection;

    public sealed class PlainJavaScriptComposer : IComposer
    {
        public void Compose(IUmbracoBuilder builder)
        {
            builder.TemplateDefinitions().Add<PlainJavaScriptTemplateDefinition>();
            builder.Services.AddSingleton<IBuildPlainJavaScriptJson, BuildPlainJavaScriptJson>();
        }
    }
}
