namespace Formulate.Core.FormHandlers.Email
{
    using Formulate.Core.Submissions.Requests;
    // Namespaces.
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Sends an email for a Formulate submission.
    /// </summary>
    internal sealed class EmailHandler : FormHandler
    {
        public EmailHandler(IFormHandlerSettings settings) : base(settings)
        {
        }

        public override void Handle(FormSubmissionRequest submission)
        {
        }
    }
}