namespace Formulate.Extensions.PlainJavaScriptTemplate.Controllers
{
    using System.Text.Json.Serialization;

    public sealed class SubmissionResponse
    {
        [JsonPropertyName(nameof(Success))]
        public bool Success { get; set; }

        [JsonPropertyName(nameof(ValidationErrors))]
        public IReadOnlyCollection<SubmissionValidationErrorResponse> ValidationErrors { get; set; } = Array.Empty<SubmissionValidationErrorResponse>();
    }
}