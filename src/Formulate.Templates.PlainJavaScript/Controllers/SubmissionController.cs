namespace Formulate.Templates.PlainJavaScript.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Formulate.Web.Utilities;

    [ApiController]
    public sealed class SubmissionController : ControllerBase
    {
        private readonly IAttemptSubmitForm _attemptSubmitForm;

        public SubmissionController(IAttemptSubmitForm attemptSubmitForm)
        {
            _attemptSubmitForm = attemptSubmitForm;
        }

        [ValidateAntiForgeryToken]
        [HttpPost("api/formulate/plain-js/submit")]
        public async Task<IActionResult> Submit([FromForm] SubmissionRequest model, CancellationToken cancellationToken)
        {
            var input = new AttemptSubmitFormInput(model.FormId, model.PageId, Request.Form, Request.Form.Files);        
            var attempt = await _attemptSubmitForm.SubmitAsync(input, cancellationToken);

            return Ok(new SubmissionResponse
            {
                Success = attempt.IsSuccessful,
                ValidationErrors = attempt.Errors.Select(x => new SubmissionValidationErrorResponse(x.FieldName, x.FieldId.ToString("N"), x.Messages)).ToArray()
            });
        }
    }
}
