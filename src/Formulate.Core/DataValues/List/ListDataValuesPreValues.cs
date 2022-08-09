namespace Formulate.Core.DataValues.List
{
    using Newtonsoft.Json;
    using System;

    /// <summary>
    /// The configuration pre-values used by a <see cref="ListDataValuesDefinition" />.
    /// </summary>
    internal sealed class ListDataValuesPreValues
    {
        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        [JsonProperty("items")]
        public ListDataValuesPreValuesItem[] Items { get; set; } = Array.Empty<ListDataValuesPreValuesItem>();
    }
}
