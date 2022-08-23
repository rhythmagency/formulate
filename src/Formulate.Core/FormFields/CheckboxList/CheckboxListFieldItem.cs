namespace Formulate.Core.FormFields.CheckboxList
{
    /// <summary>
    /// An individual item used by <see cref="CheckboxListField"/>.
    /// </summary>
    public sealed class CheckboxListFieldItem
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
