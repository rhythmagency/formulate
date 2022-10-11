namespace Formulate.BackOffice.Notifications
{
    using Formulate.BackOffice.EditorModels;
    using Umbraco.Cms.Core.Notifications;

    public sealed class SendingEditorModelNotification : INotification
    {
        public SendingEditorModelNotification(EntityEditorModel editorModel)
        {
            EditorModel = editorModel;
        }

        public EntityEditorModel EditorModel { get; init; }
    }
}
