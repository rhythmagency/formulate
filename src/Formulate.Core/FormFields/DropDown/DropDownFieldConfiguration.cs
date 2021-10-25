using System.Collections.Generic;

namespace Formulate.Core.FormFields.DropDown
{
    /// <summary>
    /// Configuration required by <see cref="DropDownField"/>.
    /// </summary>
    public sealed class DropDownFieldConfiguration
    {
        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        public IEnumerable<DropDownFieldItem> Items { get; set; }
    }
}
