namespace formulate.app.Forms.Fields.RadioButtonList
{
    using System.Collections.Generic;

    /// <summary>
    /// Configuration required by <see cref="RadioButtonListField"/>.
    /// </summary>
    public class RadioButtonListConfiguration
    {
        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        public IEnumerable<RadioButtonListItem> Items { get; set; }

        /// <summary>
        /// Gets or sets the orientation.
        /// </summary>
        public string Orientation { get; set; }
    }
}
