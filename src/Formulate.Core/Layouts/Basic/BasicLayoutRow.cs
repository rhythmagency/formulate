namespace Formulate.Core.Layouts.Basic
{
    using Newtonsoft.Json;
    using System.Collections.Generic;

    /// <summary>
    /// A row in a <see cref="BasicLayout"/>.
    /// </summary>
    public sealed class BasicLayoutRow
    {
        /// <summary>
        /// Gets a value indicating whether this starts a new step in the layout.
        /// </summary>
        [JsonProperty("isStep")]
        public bool IsStep { get; set; }

        /// <summary>
        /// Gets the cells.
        /// </summary>
        [JsonProperty("cells")]
        public IEnumerable<BasicLayoutCell> Cells { get; set; }
    }
}
