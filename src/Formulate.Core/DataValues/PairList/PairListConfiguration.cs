using System.Text.Json.Serialization;

namespace Formulate.Core.DataValues.PairList
{
    /// <summary>
    /// The configuration used by a <see cref="PairListDataValuesDefinition" />.
    /// </summary>
    internal sealed class PairListConfiguration
    {
        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        [JsonPropertyName("items")]
        public PairListConfigurationItem[] Items { get; set; }
    }
}
