namespace Formulate.Core.Submissions.Requests
{
    using Microsoft.Extensions.Primitives;

    public interface IStringFormFieldValues : IFormFieldValues
    {
        StringValues GetValues();
    }
}