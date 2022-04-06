namespace Formulate.Core.FormFields.DropDown
{
    // Namespaces.
    using System;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Configuration pre-values used by <see cref="DropDownFieldDefinition"/>
    /// for creating <see cref="DropDownField"/>.
    /// </summary>
    internal sealed class DropDownFieldPreValues
    {
        /// <summary>
        /// Gets or sets the data value.
        /// </summary>
        [JsonIgnore]
        public Guid DataValue { get; set; }

        /// <summary>
        /// Gets or sets the data value.
        /// </summary>
        /// <remarks>
        /// This is necessary to parse GUIDs that may be in multiple formats.
        /// </remarks>
        [JsonPropertyName("dataValue")]
        public string StrDataValue
        {
            get
            {
                return DataValue.ToString();
            }
            set
            {
                var parsed = Guid.Parse(value);
                DataValue = parsed;
            }
        }
    }
}