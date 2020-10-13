namespace formulate.app.Forms.Fields.Date
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using core.Utilities;

    /// <summary>
    /// A date form field type.
    /// </summary>
    public class DateField : IFormFieldType
    {
        /// <inheritdoc />
        public string Directive => "formulate-date-field";

        /// <inheritdoc />
        public string TypeLabel => "Date";

        /// <inheritdoc />
        public string Icon => "icon-formulate-date";

        /// <inheritdoc />
        public Guid TypeId => new Guid("E70CB78CCF52461198B4556382F23119");

        /// <inheritdoc />
        public object DeserializeConfiguration(string configuration)
        {
            return null;
        }

        /// <inheritdoc />
        public string FormatValue(IEnumerable<string> values, FieldPresentationFormats format, object configuration)
        {
            values = values.Select(x => new
            {
                Parsed = DateUtility.AttemptParseDate(x),
                Original = x
            }).Select(x => x.Parsed.HasValue
                ? x.Parsed.Value.ToString("MMMM dd, yyyy")
                : x.Original);
            var combined = string.Join(", ", values);
            return combined;
        }
    }
}
