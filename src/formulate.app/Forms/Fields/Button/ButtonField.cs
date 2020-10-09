namespace formulate.app.Forms.Fields.Button
{
    using Helpers;
    using Newtonsoft.Json.Linq;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// A button form field type.
    /// </summary>
    public class ButtonField : IFormFieldType, IFormFieldTypeExtended
    {
        #region Properties

        /// <inheritdoc />
        public string Directive => "formulate-button-field";

        /// <inheritdoc />
        public string TypeLabel => "Button";
        
        /// <inheritdoc />
        public string Icon => "icon-formulate-button";

        /// <inheritdoc />
        public Guid TypeId => new Guid("CDE8565C5E9241129A1F7FFA1940C53C");

        /// <inheritdoc />
        public bool IsTransitory => true;

        /// <inheritdoc />
        public bool IsServerSideOnly => false;

        /// <inheritdoc />
        public bool IsHidden => false;

        /// <inheritdoc />
        public bool IsStored => true;

        #endregion


        #region Methods

        /// <summary>
        /// Deserializes the configuration for the button field.
        /// </summary>
        /// <param name="configuration">
        /// The serialized configuration.
        /// </param>
        /// <returns>
        /// The deserialized configuration.
        /// </returns>
        public object DeserializeConfiguration(string configuration)
        {
            return JsonHelper.Deserialize<ButtonConfiguration>(configuration);
        }

        /// <summary>
        /// Formats a value in the specified field presentation format.
        /// </summary>
        /// <param name="values">
        /// The values to format.
        /// </param>
        /// <param name="format">
        /// The format to present the value in.
        /// </param>
        /// <param name="configuration">
        /// The configuration for this field.
        /// </param>
        /// <returns>
        /// The formatted value.
        /// </returns>
        public string FormatValue(IEnumerable<string> values, FieldPresentationFormats format, object configuration)
        {
            return null;
        }

        /// <summary>
        /// Is the field value valid?
        /// </summary>
        /// <param name="value">The value submitted with the form.</param>
        /// <returns>
        /// True, if the value is valid; otherwise, false.
        /// </returns>
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

        #endregion

    }
}
