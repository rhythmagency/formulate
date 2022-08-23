namespace Formulate.Website.Utilities
{
    using Formulate.Core.Submissions.Responses;

    public sealed class AttemptSubmitFormOutput
    {
        public bool IsSuccessful { get; init; }

        public IReadOnlyCollection<ValidationErrorSubmissionResponse> Errors { get; init; } = Array.Empty<ValidationErrorSubmissionResponse>();
    }
}
