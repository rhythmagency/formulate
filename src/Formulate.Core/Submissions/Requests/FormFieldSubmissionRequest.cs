namespace Formulate.Core.Submissions.Requests
{
    using Microsoft.Extensions.Primitives;
    using System;

    public sealed class FormFieldSubmissionRequest
    {
        public Guid FieldId { get; set; }

        public StringValues Values { get; set; }
    }
}
