namespace Formulate.Core.Submissions.Requests
{
    using Formulate.Core.Forms;
    using System;
    using System.Collections.Generic;

    public sealed class FormSubmissionRequest
    {
        /// <summary>
        /// Creates a new instance of a <see cref="FormSubmissionRequest"/>.
        /// </summary>
        /// <param name="form">The form.</param>
        /// <param name="pageId">The page ID.</param>
        /// <param name="fieldValues">The field values.</param>
        /// <param name="fileValues">The file values.</param>
        /// <remarks>This is the preferred constructor as it provides a new ID.</remarks>
        public FormSubmissionRequest(PersistedForm form, int pageId, Dictionary<Guid, IStringFormFieldValues> fieldValues, Dictionary<Guid, IFileFormFieldValues> fileValues) : this(Guid.NewGuid(), form, pageId, fieldValues, fileValues)
        {
        }

        /// <summary>
        /// Creates a new instance of a <see cref="FormSubmissionRequest"/>.
        /// </summary>
        /// <param name="id">The ID.</param>
        /// <param name="form">The form.</param>
        /// <param name="pageId">The page ID.</param>
        /// <param name="fieldValues">The field values.</param>
        /// <param name="fileValues">The file values.</param>
        /// <remarks>This constructor is useful if a ID is provided from another source or requires custom logic.</remarks>
        public FormSubmissionRequest(Guid id, PersistedForm form, int pageId, Dictionary<Guid, IStringFormFieldValues> fieldValues, Dictionary<Guid, IFileFormFieldValues> fileValues)
        {
            Id = id;
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

        public Guid Id { get; init; }

        public PersistedForm Form { get; init; }

        public int PageId { get; init; }

        public IReadOnlyDictionary<Guid, IStringFormFieldValues> FieldValues { get; init; } = new Dictionary<Guid, IStringFormFieldValues>();

        public IReadOnlyDictionary<Guid, IFileFormFieldValues> FilesValues { get; init; } = new Dictionary<Guid, IFileFormFieldValues>();

        public IReadOnlyCollection<KeyValuePair<Guid, IFormFieldValues>> AllValues { get; init; } = Array.Empty<KeyValuePair<Guid, IFormFieldValues>>();

        public IDictionary<string, object> ExtraContext { get; init; } = new Dictionary<string, object>();
    }
}
