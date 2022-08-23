namespace Formulate.Core.Utilities.Submissions
{
    using Formulate.Core.Submissions.Requests;
    using System.Threading;
    using System.Threading.Tasks;

    public interface ISubmitFormSubmissionRequest
    {
        Task<bool> SubmitAsync(FormSubmissionRequest input, CancellationToken cancellationToken);
    }
}
