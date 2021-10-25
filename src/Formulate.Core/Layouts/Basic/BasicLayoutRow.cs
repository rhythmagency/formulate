using System.Collections.Generic;

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
        public bool IsStep { get; set; }

        /// <summary>
        /// Gets the cells.
        /// </summary>
        public IEnumerable<BasicLayoutCell> Cells { get; set; }
    }
}
