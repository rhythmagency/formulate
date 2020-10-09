namespace formulate.app.Forms.Fields.Checkbox
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// A checkbox form field type.
    /// </summary>
    public class CheckboxField : IFormFieldType
    {
        /// <inheritdoc />
        public string Directive => "formulate-checkbox-field";
        
        /// <inheritdoc />
        public string TypeLabel => "Checkbox";

        /// <inheritdoc />
        public string Icon => "icon-checkbox-dotted";

        /// <inheritdoc />
        public Guid TypeId => new Guid("99C1A656C7A644ACA787C18593327310");

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
