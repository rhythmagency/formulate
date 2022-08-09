namespace Formulate.Core.FormFields.DropDown
{
    using Newtonsoft.Json;
    using System;

    /// <summary>
    /// Configuration pre-values used by <see cref="DropDownFieldDefinition"/>
    /// for creating <see cref="DropDownField"/>.
    /// </summary>
    internal sealed class DropDownFieldPreValues
    {
        /// <summary>
        /// Gets or sets the data value.
        /// </summary>
        [JsonProperty("dataValue")]
        public Guid DataValue { get; set; }
    }
}