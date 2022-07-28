namespace Formulate.Templates.PlainJavaScript
{
    using Microsoft.Extensions.DependencyInjection;
    using Umbraco.Cms.Core.Composing;
    using Umbraco.Cms.Core.DependencyInjection;

    public sealed class PlainJavaScriptComposer : IComposer
    {
        public void Compose(IUmbracoBuilder builder)
        {
            builder.Services.AddSingleton<IPlainJavaScriptJsonUtility, PlainJavaScriptJsonUtility>();
        }
    }
}
