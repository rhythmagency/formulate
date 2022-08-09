namespace Formulate.Core.Layouts.Basic
{
    using Newtonsoft.Json;
    using System.Collections.Generic;

    /// <summary>
    /// A cell in a basic layout row.
    /// </summary>
    public sealed class BasicLayoutCell
    {
        /// <summary>
        /// Gets the number of columns this cell spans.
        /// </summary>
        [JsonProperty("columnSpan")]
        public int ColumnSpan { get; set; }

        /// <summary>
        /// Gets the fields in this cell.
        /// </summary>
        [JsonProperty("fields")]
        public IEnumerable<BasicLayoutField> Fields { get; set; }
    }
}
