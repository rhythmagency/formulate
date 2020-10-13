namespace formulate.app.Forms.Handlers.StoreData
{
    /// <summary>
    /// A single data item used by the <see cref="StoreDataHandler"/>.
    /// </summary>
    internal class StoreDataEntry
    {
        /// <summary>
        /// Gets or sets field id.
        /// </summary>
        public string FieldId { get; set; }

        /// <summary>
        /// Gets or sets the field name.
        /// </summary>
        public string FieldName { get; set; }

        /// <summary>
        /// Gets or sets the field value.
        /// </summary>
        public string Value { get; set; }
    }
}
