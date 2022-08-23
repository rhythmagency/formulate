namespace Formulate.Core.Submissions.Requests
{
    using System.Collections.Generic;

    public sealed class FilesFormFieldValues : IFileFormFieldValues
    {
        private readonly IReadOnlyCollection<FormFileValue> _files;

        public FilesFormFieldValues(IReadOnlyCollection<FormFileValue> files)
        {
            _files = files;
        }

        public IReadOnlyCollection<FormFileValue> GetValues()
        {
            return _files;
        }
    }
}
