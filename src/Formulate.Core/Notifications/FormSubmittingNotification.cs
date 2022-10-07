namespace Formulate.Core.Notifications
{
    using Formulate.Core.Submissions.Requests;
    using Umbraco.Cms.Core.Notifications;

    /// <summary>
    /// A notification for modifying a <see cref="FormSubmissionRequest"/> before it is handled by form handlers.
    /// </summary>
    /// <remarks>This is primarily for modifying the ExtraContext property. Other propertoes in the request should be read only.</remarks>
    public sealed class FormSubmittingNotification : INotification
    {
        public FormSubmittingNotification(FormSubmissionRequest request)
        {
            Request = request;
        }

        /// <summary>
        /// Gets the request.
        /// </summary>
        public FormSubmissionRequest Request { get; init; }
    }
}
