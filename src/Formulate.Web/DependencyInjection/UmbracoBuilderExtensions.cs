namespace Formulate.Web.DependencyInjection
{
    using Formulate.Core.Persistence;
    using Formulate.Web.Configuration;
    using Formulate.Web.Persistence;
    using Formulate.Web.Utilities;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Umbraco.Cms.Core.DependencyInjection;

    public static class UmbracoBuilderExtensions
    {
        public static IUmbracoBuilder AddFormulateWebsite(this IUmbracoBuilder umbracoBuilder)
        {
            umbracoBuilder.Services.Configure<FormulateWebsiteOptions>(x => umbracoBuilder.Config.GetSection(FormulateWebsiteOptions.SectionName).Bind(x));

            umbracoBuilder.Services.AddSingleton<IRepositoryUtilityFactory, WebHostRepositoryUtilityFactory>();
            umbracoBuilder.Services.AddSingleton<IPersistedEntityCache, FileSystemPersistedEntityCache>();

            umbracoBuilder.Services.AddSingleton<IBuildFormLayoutRenderModel, BuildFormLayoutRenderModel>();
            umbracoBuilder.Services.AddScoped<IAttemptSubmitForm, AttemptSubmitForm>();

            return umbracoBuilder;
        }
    }
}
