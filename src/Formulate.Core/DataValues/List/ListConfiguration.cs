using System.Text.Json.Serialization;

namespace Formulate.Core.DataValues.List
{
    /// <summary>
    /// The configuration used by a <see cref="ListDataValuesDefinition" />.
    /// </summary>
    internal sealed class ListConfiguration
    {
        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        [JsonPropertyName("items")]
        public ListConfigurationItem[] Items { get; set; }
    }
}
