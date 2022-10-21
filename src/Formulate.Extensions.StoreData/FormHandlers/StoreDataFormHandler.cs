namespace Formulate.Extensions.StoreData.FormHandlers
{
    using Formulate.Core.FormHandlers;
    using Formulate.Core.Submissions.Requests;
    using Formulate.Core.Utilities;
    using Formulate.Extensions.StoreData.Utilities;
    using System.Threading;
    using System.Threading.Tasks;
    using Umbraco.Cms.Infrastructure.Scoping;

    /// <summary>
    /// Stores form submission data to the database.
    /// </summary>
    internal sealed class StoreDataFormHandler : AsyncFormHandler
    {
        private readonly IStoreData _storeData;

        public StoreDataFormHandler(IFormHandlerSettings settings, IStoreData storeData) : base(settings)
        {
            _storeData = storeData;
        }

        public override async Task HandleAsync(FormSubmissionRequest submission,
            CancellationToken cancellationToken = default)
        {
            await _storeData.ExecuteAsync(submission, cancellationToken);
        }
    }
}
