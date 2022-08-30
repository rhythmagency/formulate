using System.Collections.Generic;

namespace Formulate.Core.FormFields.RadioButtonList
{
    /// <summary>
    /// Configuration required by <see cref="RadioButtonListField"/>.
    /// </summary>
    public sealed class RadioButtonListFieldConfiguration
    {
        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        public IEnumerable<RadioButtonListFieldItem> Items { get; set; }

        /// <summary>
        /// Gets or sets the orientation.
        /// </summary>
        public string Orientation { get; set; }
    }
}
