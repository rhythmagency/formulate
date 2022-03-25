namespace Formulate.Core.FormHandlers.SendData
{
    // Namespaces.
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Sends form submission data to a URL.
    /// </summary>
    internal class SendDataHandler : FormHandler
    {
        public SendDataHandler(IFormHandlerSettings settings) : base(settings)
        {
            Icon = SendDataDefinition.Constants.Icon;
            Directive = SendDataDefinition.Constants.Directive;
        }

        public override void Handle(object submission)
        {
            //TODO: Implement.
            throw new NotImplementedException();
        }

        public override Task HandleAsync(object submission,
            CancellationToken cancellationToken = default)
        {
            //TODO: Implement.
            throw new NotImplementedException();
        }
    }
}