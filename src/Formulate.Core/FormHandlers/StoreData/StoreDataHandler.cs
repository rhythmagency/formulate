namespace Formulate.Core.FormHandlers.StoreData
{
    using Formulate.Core.Submissions.Requests;
    // Namespaces.
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Stores form submission data to the database.
    /// </summary>
    internal sealed class StoreDataHandler : FormHandler
    {
        public StoreDataHandler(IFormHandlerSettings settings) : base(settings)
        {
        }

        public override async Task Handle(FormSubmissionRequest submission,
            CancellationToken cancellationToken = default)
        {
            await Task.Run(() => "To Do");
        }
    }
}