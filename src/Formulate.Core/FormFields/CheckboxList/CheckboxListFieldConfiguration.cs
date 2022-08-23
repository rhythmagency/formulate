using System.Collections.Generic;

namespace Formulate.Core.FormFields.CheckboxList
{
    /// <summary>
    /// Configuration required by <see cref="CheckboxListField"/>.
    /// </summary>
    public sealed class CheckboxListFieldConfiguration
    {
        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        public IEnumerable<CheckboxListFieldItem> Items { get; set; }
    }
}
