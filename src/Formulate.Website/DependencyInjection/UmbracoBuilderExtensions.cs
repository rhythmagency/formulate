namespace Formulate.Website.DependencyInjection
{
    using Formulate.Website.Utilities;
    using Microsoft.Extensions.DependencyInjection;
    using Umbraco.Cms.Core.DependencyInjection;

    public static class UmbracoBuilderExtensions
    {
        public static IUmbracoBuilder AddFormulateWebsite(this IUmbracoBuilder umbracoBuilder)
        {
            umbracoBuilder.Services.AddSingleton<IPlainJavaScriptJsonUtility, PlainJavaScriptJsonUtility>();
            umbracoBuilder.Services.AddSingleton<IBuildConfiguredFormRenderModel, BuildConfiguredFormRenderModel>();

            return umbracoBuilder;
        }
    }
}
