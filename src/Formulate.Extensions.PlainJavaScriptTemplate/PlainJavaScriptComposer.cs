namespace Formulate.Extensions.PlainJavaScriptTemplate
{
    using Formulate.Core.DependencyInjection;
    using Formulate.Extensions.PlainJavaScriptTemplate.Core;
    using Microsoft.Extensions.DependencyInjection;
    using Umbraco.Cms.Core.Composing;
    using Umbraco.Cms.Core.DependencyInjection;

    public sealed class PlainJavaScriptComposer : IComposer
    {
        public void Compose(IUmbracoBuilder builder)
        {
            builder.AddPackageManifest();
            builder.AddMapDefinitions();

            builder.TemplateDefinitions().Append<PlainJavaScriptTemplateDefinition>();
            builder.Services.AddSingleton<IBuildPlainJavaScriptJson, BuildPlainJavaScriptJson>();
        }
    }
}
