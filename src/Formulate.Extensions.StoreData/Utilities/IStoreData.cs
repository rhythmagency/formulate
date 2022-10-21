namespace Formulate.Extensions.StoreData.Utilities
{
    using Formulate.Core.Submissions.Requests;

    public interface IStoreData
    {
        Task ExecuteAsync(FormSubmissionRequest submission, CancellationToken cancellationToken);
    }
}