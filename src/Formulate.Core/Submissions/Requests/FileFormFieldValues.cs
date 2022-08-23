namespace Formulate.Core.Submissions.Requests
{
    using System.Collections.Generic;

    public sealed class FileFormFieldValues : IFileFormFieldValues
    {
        private readonly IReadOnlyCollection<FormFileValue> _files;

        public FileFormFieldValues(FormFileValue file)
        {
            _files = new[] { file };
        }

        public IReadOnlyCollection<FormFileValue> GetValues()
        {
            return _files;
        }
    }
}
