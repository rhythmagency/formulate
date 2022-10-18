namespace Formulate.Extensions.PlainJavaScriptTemplate.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    public sealed class SubmissionValidationErrorResponse
    {
        public SubmissionValidationErrorResponse(string fieldName, string fieldId, string message)
        {
            FieldName = fieldName;
            FieldId = fieldId;
            Messages = new[] { message };
        }

        public SubmissionValidationErrorResponse(string fieldName, string fieldId, IReadOnlyCollection<string> messages)
        {
            FieldName = fieldName;
            FieldId = fieldId;
            Messages = messages;
        }

        [JsonPropertyName(nameof(FieldName))]
        public string FieldName { get; set; }

        [JsonPropertyName(nameof(FieldId))]
        public string FieldId { get; set; }

        [JsonPropertyName(nameof(Messages))]
        public IReadOnlyCollection<string> Messages { get; set; } = Array.Empty<string>();
    }
}
