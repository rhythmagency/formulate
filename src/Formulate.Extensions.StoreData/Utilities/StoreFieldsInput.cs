namespace Formulate.Extensions.StoreData.Utilities
{
    using Formulate.Core.Forms;
    using Formulate.Core.Submissions.Requests;
    using System;

    public sealed class StoreFieldsInput
    {
        public StoreFieldsInput(FormSubmissionRequest request) : this(request.Form, request.FieldValues)
        {
        }

        public StoreFieldsInput(PersistedForm form, IReadOnlyDictionary<Guid, IStringFormFieldValues> fields)
        {
            Form = form;
            Fields = fields;
        }

        public PersistedForm Form { get; init; }

        public IReadOnlyDictionary<Guid, IStringFormFieldValues> Fields { get; init; } = new Dictionary<Guid, IStringFormFieldValues>();
    }
}