using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Formulate.Core.Layouts.Basic
{
    /// <summary>
    /// A cell in a basic layout row.
    /// </summary>
    public sealed class BasicLayoutCell
    {
        /// <summary>
        /// Gets the number of columns this cell spans.
        /// </summary>
        [JsonPropertyName("columnSpan")]
        public int ColumnSpan { get; set; }

        /// <summary>
        /// Gets the fields in this cell.
        /// </summary>
        [JsonPropertyName("fields")]
        public IEnumerable<BasicLayoutField> Fields { get; set; }
    }
}
