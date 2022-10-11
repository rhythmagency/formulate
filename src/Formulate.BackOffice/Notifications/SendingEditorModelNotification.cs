namespace Formulate.BackOffice.Notifications
{
    using Formulate.BackOffice.EditorModels;
    using Umbraco.Cms.Core.Notifications;

    public sealed class SendingEditorModelNotification : INotification
    {
        public SendingEditorModelNotification(IEditorModel? editorModel)
        {
            EditorModel = editorModel;
        }

        public IEditorModel? EditorModel { get; init; }
    }
}
