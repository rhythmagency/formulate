namespace formulate.app.Forms.Fields.ExtendedRadioButtonList
{
    /// <summary>
    /// An individual item used by <see cref="ExtendedRadioButtonListField"/>.
    /// </summary>
    public class ExtendedRadioButtonListItem
    {
        /// <summary>
        /// Gets or sets a value indicating whether this item is selected.
        /// </summary>
        public bool Selected { get; set; }

        /// <summary>
        /// Gets or sets the primary.
        /// </summary>
        public string Primary { get; set; }
        
        /// <summary>
        /// Gets or sets the secondary.
        /// </summary>
        public string Secondary { get; set; }
    }
}
