using Formulate.BackOffice.Persistence;
using Microsoft.Extensions.DependencyInjection;
using Umbraco.Cms.Core.DependencyInjection;

namespace Formulate.BackOffice.DependencyInjection
{
    public static class UmbracoBuilderExtensions
    {
        public static IUmbracoBuilder AddFormulateBackOffice(this IUmbracoBuilder builder)
        {
            builder.Sections().Append<FormulateSection>();
            builder.Services.AddScoped<ITreeEntityRepository, TreeEntityRepository>();


            return builder;
        }
    }
}
