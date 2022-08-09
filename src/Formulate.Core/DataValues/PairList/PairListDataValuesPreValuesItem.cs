namespace Formulate.Core.DataValues.PairList
{
    using Newtonsoft.Json;

    /// <summary>
    /// An item used by the <see cref="PairListDataValuesPreValues"/>.
    /// </summary>
    internal sealed class PairListDataValuesPreValuesItem
    {
        /// <summary>
        /// Gets or sets the secondary value.
        /// </summary>
        [JsonProperty("secondary")]
        public string Secondary { get; set; }

        /// <summary>
        /// Gets or sets the primary value.
        /// </summary>
        [JsonProperty("primary")]
        public string Primary { get; set; }
    }
}
