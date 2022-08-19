namespace Formulate.BackOffice.DependencyInjection
{
    using Formulate.BackOffice.NotificationHandlers;
    using Umbraco.Cms.Core.DependencyInjection;
    using Umbraco.Cms.Core.Notifications;

    public static partial class UmbracoBuilderExtensions
    {
        /// <summary>
        /// Adds Formulate Notification Handlers.
        /// </summary>
        /// <param name="builder">
        /// The Umbraco builder.
        /// </param>
        /// <returns>
        /// The current <see cref="IUmbracoBuilder"/>.
        /// </returns>
        private static IUmbracoBuilder AddFormulateNotificationHandlers(
            this IUmbracoBuilder builder)
        {
            builder.AddNotificationHandler<ServerVariablesParsingNotification, ServerVariablesNotificationHandler>();

            return builder;
        }
    }
}
