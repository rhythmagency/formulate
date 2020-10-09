namespace formulate.app.Forms.Fields.RadioButtonList
{
    /// <summary>
    /// An individual item used by <see cref="RadioButtonListField"/>.
    /// </summary>
    public class RadioButtonListItem
    {
        /// <summary>
        /// Gets or sets a value indicating whether this item is selected.
        /// </summary>
        public bool Selected { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets the label.
        /// </summary>
        public string Label { get; set; }
    }
}