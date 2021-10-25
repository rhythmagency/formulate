using System;
using System.Collections.Generic;

namespace Formulate.Core.Layouts.Basic
{
    /// <summary>
    /// The configuration for a <see cref="BasicLayout"/>.
    /// </summary>
    public sealed class BasicLayoutConfiguration
    {
        /// <summary>
        /// Gets a value indicating whether to automatically populate this layout based on changes to the form.
        /// </summary>
        public bool AutoPopulate { get; set; }

        /// <summary>
        /// Gets the form ID.
        /// </summary>
        public Guid? FormId { get; set; }

        /// <summary>
        /// Gets the rows.
        /// </summary>
        public IEnumerable<BasicLayoutRow> Rows { get; set; }
    }
}
