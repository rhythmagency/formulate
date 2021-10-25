using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Formulate.Core.Layouts.Basic
{
    /// <summary>
    /// A row in a <see cref="BasicLayout"/>.
    /// </summary>
    public sealed class BasicLayoutRow
    {
        /// <summary>
        /// Gets a value indicating whether this starts a new step in the layout.
        /// </summary>
        [JsonPropertyName("isStep")]
        public bool IsStep { get; set; }

        /// <summary>
        /// Gets the cells.
        /// </summary>
        [JsonPropertyName("cells")]
        public IEnumerable<BasicLayoutCell> Cells { get; set; }
    }
}
