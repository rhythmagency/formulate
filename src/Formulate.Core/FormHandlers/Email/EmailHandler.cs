namespace Formulate.Core.FormHandlers.Email
{
    // Namespaces.
    using System;

    /// <summary>
    /// Sends an email for a Formulate submission.
    /// </summary>
    internal class EmailHandler : FormHandler
    {
        public EmailHandler(IFormHandlerSettings settings) : base(settings)
        {
            Icon = EmailDefinition.Constants.Icon;
            Directive = EmailDefinition.Constants.Directive;
        }

        public override void Handle(object submission)
        {
            //TODO: Implement.
            throw new NotImplementedException();
        }
    }
}