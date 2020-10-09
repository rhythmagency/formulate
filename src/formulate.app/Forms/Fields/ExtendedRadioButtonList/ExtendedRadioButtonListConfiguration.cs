namespace formulate.app.Forms.Fields.ExtendedRadioButtonList
{
    using System.Collections.Generic;

    /// <summary>
    /// Configuration required by <see cref="ExtendedRadioButtonListField"/>.
    /// </summary>
    public class ExtendedRadioButtonListConfiguration
    {
        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        public IEnumerable<ExtendedRadioButtonListItem> Items { get; set; }
    }
}
