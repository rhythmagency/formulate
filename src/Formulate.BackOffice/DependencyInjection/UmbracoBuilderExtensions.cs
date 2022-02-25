using Formulate.BackOffice.NotificationHandlers;
using Formulate.BackOffice.Persistence;
using Microsoft.Extensions.DependencyInjection;
using Umbraco.Cms.Core.DependencyInjection;
using Umbraco.Cms.Core.Notifications;

namespace Formulate.BackOffice.DependencyInjection
{
    public static class UmbracoBuilderExtensions
    {
        public static IUmbracoBuilder AddFormulateBackOffice(this IUmbracoBuilder builder)
        {
            builder.Sections().Append<FormulateSection>();

            builder.Services.AddScoped<ITreeEntityRepository, TreeEntityRepository>();

            builder.AddNotificationHandler<ServerVariablesParsingNotification, ServerVariablesNotificationHandler>();

            return builder;
        }
    }
}
