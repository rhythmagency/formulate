namespace formulate.app.Forms.Fields.Text
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// An upload form field type.
    /// </summary>
    public class UploadField : IFormFieldType
    {
        /// <inheritdoc />
        public string Directive => "formulate-upload-field";

        /// <inheritdoc />
        public string TypeLabel => "Upload";

        /// <inheritdoc />
        public string Icon => "icon-formulate-upload";

        /// <inheritdoc />
        public Guid TypeId => new Guid("DFEFA5EC02004806A2AB0AB22058021D");

        /// <inheritdoc />
        public object DeserializeConfiguration(string configuration)
        {
            return null;
        }

        /// <inheritdoc />
        public string FormatValue(IEnumerable<string> values, FieldPresentationFormats format, object configuration)
        {
            return null;
        }
    }
}
