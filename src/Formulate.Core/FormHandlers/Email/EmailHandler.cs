namespace Formulate.Core.FormHandlers.Email
{
    // Namespaces.
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Sends an email for a Formulate submission.
    /// </summary>
    internal class EmailHandler : FormHandler
    {
        public EmailHandler(IFormHandlerSettings settings) : base(settings)
        {
        }

        public override Task Handle(object submission,
            CancellationToken cancellationToken = default)
        {
            //TODO: Implement.
            throw new NotImplementedException();
        }
    }
}