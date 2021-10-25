using System.Text.Json.Serialization;

namespace Formulate.Core.DataValues.PairList
{
    /// <summary>
    /// An item used by the <see cref="PairListConfiguration"/>.
    /// </summary>
    internal sealed class PairListConfigurationItem
    {
        /// <summary>
        /// Gets or sets the label.
        /// </summary>
        [JsonPropertyName("secondary")]
        public string Label { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        [JsonPropertyName("primary")]
        public string Value { get; set; }
    }
}
