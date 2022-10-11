namespace Formulate.BackOffice.NotificationHandlers
{
    using Formulate.BackOffice.EditorModels;
    using Formulate.BackOffice.Notifications;
    using System.Collections.Generic;
    using System.Linq;
    using Umbraco.Cms.Core.ContentApps;
    using Umbraco.Cms.Core.Events;
    using Umbraco.Cms.Core.Models.ContentEditing;

    public sealed class SendingEditorModelNotificationHandler : INotificationHandler<SendingEditorModelNotification>
    {
        private readonly ContentAppFactoryCollection _contentAppDefinitions;

        public SendingEditorModelNotificationHandler(ContentAppFactoryCollection contentAppDefinitions)
        {
            _contentAppDefinitions = contentAppDefinitions;
        }

        public void Handle(SendingEditorModelNotification notification)
        {
            notification.EditorModel.Apps = GetContentApps(notification.EditorModel);
        }

        private IReadOnlyCollection<ContentApp> GetContentApps(EntityEditorModel editorModel)
        {
            var apps = _contentAppDefinitions.GetContentAppsFor(editorModel).OrderBy(x => x.Weight).ToArray();
            var processedApps = new List<ContentApp>();
            var hasProcessedFirstApp = false;

            foreach (var app in apps)
            {
                if (hasProcessedFirstApp)
                {
                    app.Active = false;
                }
                else
                {
                    app.Active = true;
                    hasProcessedFirstApp = true;
                }

                processedApps.Add(app);
            }

            return processedApps.ToArray();
        }
    }
}
