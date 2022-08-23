namespace Formulate.Core.Submissions.Requests
{
    using Microsoft.Extensions.Primitives;
    using System;
    using System.Collections.Generic;

    internal sealed class EmptyFormFieldValues : IFileFormFieldValues, IStringFormFieldValues
    {
        StringValues IStringFormFieldValues.GetValues()
        {
            return StringValues.Empty;
        }

        IReadOnlyCollection<FormFileValue> IFileFormFieldValues.GetValues()
        {
            return Array.Empty<FormFileValue>();
        }
    }
}