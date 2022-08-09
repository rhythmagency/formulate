namespace Formulate.Core.ConfiguredForms
{
    using Newtonsoft.Json;
    using System;

    /// <summary>
    /// The property value stored by a Confirmed Form Picker.
    /// </summary>
    public sealed class ConfiguredFormPropertyValue
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }
    }
}
