namespace Formulate.Core.DataValues.List
{
    using Newtonsoft.Json;

    /// <summary>
    /// An item used by the <see cref="ListDataValuesPreValues"/>.
    /// </summary>
    internal sealed class ListDataValuesPreValuesItem
    {
        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        [JsonProperty("value")]
        public string Value { get; set; }
    }
}
