namespace Formulate.Core.DataValues.PairList
{
    using Newtonsoft.Json;
    using System;

    /// <summary>
    /// The configuration pre-values used by a <see cref="PairListDataValuesDefinition" />.
    /// </summary>
    internal sealed class PairListDataValuesPreValues
    {
        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        [JsonProperty("items")]
        public PairListDataValuesPreValuesItem[] Items { get; set; } = Array.Empty<PairListDataValuesPreValuesItem>(); 
    }
}
