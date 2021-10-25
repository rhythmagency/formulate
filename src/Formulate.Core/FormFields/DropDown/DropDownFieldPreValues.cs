using System;
using System.Text.Json.Serialization;

namespace Formulate.Core.FormFields.DropDown
{
    /// <summary>
    /// Configuration pre-values used by <see cref="DropDownFieldDefinition"/> for creating <see cref="DropDownField"/>.
    /// </summary>
    public sealed class DropDownFieldPreValues
    {
        /// <summary>
        /// Gets or sets the data value.
        /// </summary>
        [JsonPropertyName("dataValue")]
        public Guid DataValue { get; set; }
    }
}
