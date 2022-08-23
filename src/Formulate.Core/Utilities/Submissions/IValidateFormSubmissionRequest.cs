namespace Formulate.Core.Utilities.Submissions
{
    using Formulate.Core.Submissions.Requests;

    public interface IValidateFormSubmissionRequest
    {
        ValidateFormSubmissionRequestOutput Validate(FormSubmissionRequest input);
    }
}
