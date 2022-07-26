namespace Formulate.Core.ConfiguredForms
{
    using System;
    using System.Text.Json.Serialization;

    /// <summary>
    /// The property value stored by a Confirmed Form Picker.
    /// </summary>
    public sealed class ConfiguredFormPropertyValue
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
    }
}
