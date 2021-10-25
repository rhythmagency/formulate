using System.Text.Json.Serialization;

namespace Formulate.Core.DataValues.List
{
    /// <summary>
    /// The configuration pre-values used by a <see cref="ListDataValuesDefinition" />.
    /// </summary>
    internal sealed class ListDataValuesPreValues
    {
        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        [JsonPropertyName("items")]
        public ListDataValuesPreValuesItem[] Items { get; set; }
    }
}
