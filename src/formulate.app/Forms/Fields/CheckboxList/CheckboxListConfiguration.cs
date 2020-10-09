namespace formulate.app.Forms.Fields.CheckboxList
{
    using System.Collections.Generic;
    
    /// <summary>
    /// Configuration required by <see cref="CheckboxListField"/>.
    /// </summary>
    public class CheckboxListConfiguration
    {
        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        public IEnumerable<CheckboxListItem> Items { get; set; }
    }
}