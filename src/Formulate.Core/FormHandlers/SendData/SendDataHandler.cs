namespace Formulate.Core.FormHandlers.SendData
{
    using Formulate.Core.Submissions.Requests;
    // Namespaces.
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Sends form submission data to a URL.
    /// </summary>
    internal sealed class SendDataHandler : FormHandler
    {
        public SendDataHandler(IFormHandlerSettings settings) : base(settings)
        {
        }

        public override async Task Handle(FormSubmissionRequest submission,
            CancellationToken cancellationToken = default)
        {
            await Task.Run(() => "To Do");
        }
    }
}