namespace Formulate.Core.FormHandlers.StoreData
{
    // Namespaces.
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Stores form submission data to the database.
    /// </summary>
    internal class StoreDataHandler : FormHandler
    {
        public StoreDataHandler(IFormHandlerSettings settings) : base(settings)
        {
            Icon = StoreDataDefinition.Constants.Icon;
            Directive = StoreDataDefinition.Constants.Directive;
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