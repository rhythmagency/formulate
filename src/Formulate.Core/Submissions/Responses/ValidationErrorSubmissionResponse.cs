namespace Formulate.Core.Submissions.Responses
{
    using System;
    using System.Collections.Generic;

    public sealed class ValidationErrorSubmissionResponse
    {
        public ValidationErrorSubmissionResponse(string fieldName, Guid fieldId, string message)
        {
            FieldName = fieldName;
            FieldId = fieldId;
            Messages = new[] { message };
        }

        public ValidationErrorSubmissionResponse(string fieldName, Guid fieldId, IReadOnlyCollection<string> messages)
        {
            FieldName = fieldName;
            FieldId = fieldId;
            Messages = messages;
        }

        public string FieldName { get; set; }

        public Guid FieldId { get; set; }

        public IReadOnlyCollection<string> Messages { get; set; } = Array.Empty<string>();
    }
}
