namespace Formulate.Core.Submissions.Requests
{
    using System.Collections.Generic;

    public interface IFileFormFieldValues : IFormFieldValues
    {
        IReadOnlyCollection<FormFileValue> GetValues();
    }
}