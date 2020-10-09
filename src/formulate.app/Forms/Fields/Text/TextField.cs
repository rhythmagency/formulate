namespace formulate.app.Forms.Fields.Text
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// A text form field type.
    /// </summary>
    public class TextField : IFormFieldType
    {
        /// <inheritdoc />
        public string Directive => "formulate-text-field";

        /// <inheritdoc />
        public string TypeLabel => "Text";

        /// <inheritdoc />
        public string Icon => "icon-document-dashed-line";

        /// <inheritdoc />
        public Guid TypeId => new Guid("1790658086EA440BBC309E1B099F803B");

        /// <inheritdoc />
        public object DeserializeConfiguration(string configuration)
        {
            return null;
        }

        /// <inheritdoc />
        public string FormatValue(IEnumerable<string> values, FieldPresentationFormats format, object configuration)
        {
            return string.Join(", ", values);
        }
    }
}
