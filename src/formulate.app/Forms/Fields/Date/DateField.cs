namespace formulate.app.Forms.Fields.Date
{
    using core.Utilities;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    public class DateField : IFormFieldType
    {
        public string Directive => "formulate-date-field";
        public string TypeLabel => "Date";
        public string Icon => "icon-formulate-date";
        public Guid TypeId => new Guid("E70CB78CCF52461198B4556382F23119");
        public object DeserializeConfiguration(string configuration)
        {
            return null;
        }
        public string FormatValue(IEnumerable<string> values, FieldPresentationFormats format)
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