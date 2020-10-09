namespace formulate.app.Forms.Fields.TextConstant
{

    // Namespaces.
    using Helpers;
    using Newtonsoft.Json.Linq;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// A form field type that can be used to store a text constant.
    /// </summary>
    /// <remarks>
    /// A text constant field is useful, for example, when you need to store a bit of text
    /// in a field so that it can be sent as a parameter to a web API, but you otherwise
    /// don't need the field to be output to the page when the form is rendered.
    /// </remarks>
    public class TextConstantField : IFormFieldType, IFormFieldTypeExtended
    {

        #region Properties
        
        /// <inheritdoc />
        public string Directive => "formulate-text-constant-field";

        /// <inheritdoc />
        public string TypeLabel => "Text Constant";

        /// <inheritdoc />
        public string Icon => "icon-formulate-text-constant";

        /// <inheritdoc />
        public Guid TypeId => new Guid("D9B1A60A11864440887B93195C760B5E");

        /// <inheritdoc />
        public bool IsTransitory => true;

        /// <inheritdoc />
        public bool IsServerSideOnly => true;

        /// <inheritdoc />
        public bool IsHidden => false;

        /// <inheritdoc />
        public bool IsStored => true;

        #endregion


        #region Methods

        /// <summary>
        /// Deserializes the configuration for the text constant field.
        /// </summary>
        /// <param name="configuration">
        /// The serialized configuration.
        /// </param>
        /// <returns>
        /// The deserialized configuration.
        /// </returns>
        public object DeserializeConfiguration(string configuration)
        {

            // Variables.
            var config = new TextConstantConfiguration()
            {
                Text = null
            };
            var configData = JsonHelper.Deserialize<JObject>(configuration);
            var dynamicConfig = configData as dynamic;
            var properties = configData.Properties().Select(x => x.Name);
            var propertySet = new HashSet<string>(properties);


            // Text value exists?
            if (propertySet.Contains("text"))
            {
                config.Text = dynamicConfig.text.Value as string;
            }

            // Return the configuration.
            return config;
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
            var castedConfig = configuration as TextConstantConfiguration;
            return castedConfig?.Text;
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
