namespace Formulate.Website.DependencyInjection
{
    using Formulate.Core.Persistence;
    using Formulate.Website.Configuration;
    using Formulate.Website.Persistence;
    using Formulate.Website.Utilities;
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
