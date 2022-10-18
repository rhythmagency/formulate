namespace Formulate.Extensions.SendEmail.FormHandlers
{

    // Namespaces.
    using Formulate.Core.FormHandlers;
    using Formulate.Core.Submissions.Requests;

    /// <summary>
    /// Sends an email for a Formulate submission.
    /// </summary>
    internal sealed class SendEmailFormHandler : FormHandler
    {
        public SendEmailFormHandler(IFormHandlerSettings settings) : base(settings)
        {
        }

        public override void Handle(FormSubmissionRequest submission)
        {
        }
    }
}