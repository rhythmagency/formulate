namespace Formulate.Extensions.StoreData.Utilities
{
    using Formulate.Core.Forms;
    using Formulate.Core.Submissions.Requests;
    using System;

    public sealed class StoreFilesInput
    {
        public StoreFilesInput(FormSubmissionRequest request) : this(request.Form, request.Id, request.FilesValues)
        {
        }

        public StoreFilesInput(PersistedForm form, Guid submissionId, IReadOnlyDictionary<Guid, IFileFormFieldValues> files)
        {
            Form = form;
            SubmissionId = submissionId;
            Files = files;
        }

        public Guid SubmissionId { get; internal set; }

        public IReadOnlyDictionary<Guid, IFileFormFieldValues> Files { get; set; }

        public PersistedForm Form { get; internal set; }
    }
}