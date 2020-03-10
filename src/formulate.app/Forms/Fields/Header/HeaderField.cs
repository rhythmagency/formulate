namespace formulate.app.Forms.Fields.Header
{

    // Namespaces.
    using Helpers;
    using Newtonsoft.Json.Linq;
    using System;
    using System.Collections.Generic;
    using System.Linq;


    /// <summary>
    /// A field that can be used to display a header in a form.
    /// </summary>
    public class HeaderField : IFormFieldType, IFormFieldTypeExtended
    {

        #region Properties

        public string Directive => "formulate-header-field";
        public string TypeLabel => "Header";
        public string Icon => "icon-formulate-header";
        public Guid TypeId => new Guid("6383DD2C68BD482B95DB811D09D01BC8");
        public bool IsTransitory => true;
        public bool IsServerSideOnly => false;
        public bool IsHidden => false;
        public bool IsStored => false;

        #endregion


        #region Methods

        /// <summary>
        /// Deserializes the configuration for the header field.
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
            var config = new HeaderConfiguration()
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
        public string FormatValue(IEnumerable<string> values, FieldPresentationFormats format,
            object configuration)
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
        /// Null.
        /// </returns>
        public string GetNativeFieldValidationMessage() => null;

        #endregion

    }

}