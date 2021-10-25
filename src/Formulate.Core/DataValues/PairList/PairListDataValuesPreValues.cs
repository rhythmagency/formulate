using System.Text.Json.Serialization;

namespace Formulate.Core.DataValues.PairList
{
    /// <summary>
    /// The configuration pre-values used by a <see cref="PairListDataValuesDefinition" />.
    /// </summary>
    internal sealed class PairListDataValuesPreValues
    {
        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        [JsonPropertyName("items")]
        public PairListDataValuesPreValuesItem[] Items { get; set; }
    }
}
