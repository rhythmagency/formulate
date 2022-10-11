namespace Formulate.BackOffice.NotificationHandlers
{
    using Formulate.BackOffice.EditorModels;
    using Formulate.BackOffice.EditorModels.Forms;
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
            if (notification.EditorModel is null)
            {
                return;
            }

            switch (notification.EditorModel)
            {
                case FormEditorModel editorModel:
                    notification.EditorModel.Apps = GetContentApps(notification.EditorModel);

                    foreach (var field in editorModel.Fields)
                    {
                        field.Apps = GetContentApps(field);
                    }

                    foreach (var handler in editorModel.Handlers)
                    {
                        handler.Apps = GetContentApps(handler);
                    }

                    break;
                default:
                    notification.EditorModel.Apps = GetContentApps(notification.EditorModel);
                    break;
            }
        }

        private IReadOnlyCollection<ContentApp> GetContentApps(IEditorModel editorModel)
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
