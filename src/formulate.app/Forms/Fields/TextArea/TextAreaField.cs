namespace formulate.app.Forms.Fields.Text
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// A text area form field type.
    /// </summary>
    public class TextAreaField : IFormFieldType
    {
        /// <inheritdoc />
        public string Directive => "formulate-text-area-field";

        /// <inheritdoc />
        public string TypeLabel => "Text Area";

        /// <inheritdoc />
        public string Icon => "icon-formulate-textarea";

        /// <inheritdoc />
        public Guid TypeId => new Guid("9DA843594D0B494491449F8CCAE7A4DA");

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
