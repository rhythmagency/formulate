using System.Text.Json.Serialization;

namespace Formulate.Core.DataValues.PairList
{
    /// <summary>
    /// An item used by the <see cref="PairListDataValuesPreValues"/>.
    /// </summary>
    internal sealed class PairListDataValuesPreValuesItem
    {
        /// <summary>
        /// Gets or sets the secondary value.
        /// </summary>
        [JsonPropertyName("secondary")]
        public string Secondary { get; set; }

        /// <summary>
        /// Gets or sets the primary value.
        /// </summary>
        [JsonPropertyName("primary")]
        public string Primary { get; set; }
    }
}
