using System.Collections.Generic;

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
        public int ColumnSpan { get; set; }

        /// <summary>
        /// Gets the fields in this cell.
        /// </summary>
        public IEnumerable<BasicLayoutField> Fields { get; set; }
    }
}
