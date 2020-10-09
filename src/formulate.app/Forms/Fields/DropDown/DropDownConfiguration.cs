namespace formulate.app.Forms.Fields.DropDown
{
    using System.Collections.Generic;

    /// <summary>
    /// Configuration required by <see cref="DropDownField"/>.
    /// </summary>
    public class DropDownConfiguration
    {
        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        public IEnumerable<DropDownItem> Items { get; set; }
    }
}