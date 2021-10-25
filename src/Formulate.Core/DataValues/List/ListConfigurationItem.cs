using System.Text.Json.Serialization;

namespace Formulate.Core.DataValues.List
{
    /// <summary>
    /// An item used by the <see cref="ListConfiguration"/>.
    /// </summary>
    internal sealed class ListConfigurationItem
    {
        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        [JsonPropertyName("value")]
        public string Value { get; set; }
    }
}
