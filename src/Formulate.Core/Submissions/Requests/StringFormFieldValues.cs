namespace Formulate.Core.Submissions.Requests
{
    using Microsoft.Extensions.Primitives;

    public sealed class StringFormFieldValues : IStringFormFieldValues
    {
        private readonly StringValues _values;

        public StringFormFieldValues(StringValues values)
        {
            _values = values;
        }

        public StringValues GetValues()
        {
            return _values;
        }
    }
}
