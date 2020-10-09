namespace formulate.app.Forms.Fields.Hidden
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// A hidden form field type.
    /// </summary>
    public class HiddenField : IFormFieldType, IFormFieldTypeExtended
    {
        /// <inheritdoc />
        public string Directive => "formulate-hidden-field";

        /// <inheritdoc />
        public string TypeLabel => "Hidden";

        /// <inheritdoc />
        public string Icon => "icon-formulate-hidden";

        /// <inheritdoc />
        public Guid TypeId => new Guid("3DF6FACD2FFA4055B0BE94E8FA8E7C4A");

        /// <inheritdoc />
        public bool IsTransitory => false;

        /// <inheritdoc />
        public bool IsServerSideOnly => false;

        /// <inheritdoc />
        public bool IsHidden => true;

        /// <inheritdoc />
        public bool IsStored => true;

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

        /// <inheritdoc />
        public bool IsValid(IEnumerable<string> value)
        {
            return true;
        }

        /// <summary>
        /// Returns null (part of the interface, but not required for this field type).
        /// </summary>
        /// <returns>
        /// A null value.
        /// </returns>
        public string GetNativeFieldValidationMessage() => null;
    }
}
