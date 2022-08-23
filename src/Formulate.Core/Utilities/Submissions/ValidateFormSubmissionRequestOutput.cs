namespace Formulate.Core.Utilities.Submissions
{
    using Formulate.Core.Submissions.Responses;
    using System;
    using System.Collections.Generic;

    public sealed class ValidateFormSubmissionRequestOutput
    {
        public IReadOnlyCollection<ValidationErrorSubmissionResponse> Errors { get; set; } = Array.Empty<ValidationErrorSubmissionResponse>();
    }
}
