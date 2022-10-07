namespace Formulate.Core.Submissions.Requests
{
    using Formulate.Core.Forms;
    using System;
    using System.Collections.Generic;

    public sealed class FormSubmissionRequest
    {
        public FormSubmissionRequest(PersistedForm form, int pageId, Dictionary<Guid, IStringFormFieldValues> fieldValues, Dictionary<Guid, IFileFormFieldValues> fileValues)
        {
            Form = form;
            PageId = pageId;
            FieldValues = fieldValues;
            FilesValues = fileValues;

            var allValues = new List<KeyValuePair<Guid, IFormFieldValues>>();

            foreach (var fieldValue in fieldValues)
            {
                allValues.Add(new KeyValuePair<Guid, IFormFieldValues>(fieldValue.Key, fieldValue.Value));
            }

            foreach (var fieldValue in fileValues)
            {
                allValues.Add(new KeyValuePair<Guid, IFormFieldValues>(fieldValue.Key, fieldValue.Value));
            }

            AllValues = allValues;
        }

        public PersistedForm Form { get; init; }

        public int PageId { get; init; }

        public IReadOnlyDictionary<Guid, IStringFormFieldValues> FieldValues { get; init; } = new Dictionary<Guid, IStringFormFieldValues>();

        public IReadOnlyDictionary<Guid, IFileFormFieldValues> FilesValues { get; init; } = new Dictionary<Guid, IFileFormFieldValues>();

        public IReadOnlyCollection<KeyValuePair<Guid, IFormFieldValues>> AllValues { get; init; } = Array.Empty<KeyValuePair<Guid, IFormFieldValues>>();

        public IDictionary<string, object> ExtraContext { get; init; } = new Dictionary<string, object>();
    }
}
