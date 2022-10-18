namespace Formulate.Extensions.StoreData.FormHandlers
{
    using Formulate.Core.FormHandlers;
    using Formulate.Core.Submissions.Requests;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Stores form submission data to the database.
    /// </summary>
    internal sealed class StoreDataFormHandler : AsyncFormHandler
    {
        public StoreDataFormHandler(IFormHandlerSettings settings) : base(settings)
        {
        }

        public override async Task HandleAsync(FormSubmissionRequest submission,
            CancellationToken cancellationToken = default)
        {
            await Task.Run(() => "To Do");
        }
    }
}
