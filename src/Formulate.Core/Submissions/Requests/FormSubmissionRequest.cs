namespace Formulate.Core.Submissions.Requests
{
    using Formulate.Core.Forms;
    using System;
    using System.Collections.Generic;

    public sealed class FormSubmissionRequest
    {
        public PersistedForm Form { get; set; }

        public int PageId { get; set; }

        public Dictionary<Guid, IFormFieldValues> FieldValues { get; set; } = new Dictionary<Guid, IFormFieldValues>();
    }
}
