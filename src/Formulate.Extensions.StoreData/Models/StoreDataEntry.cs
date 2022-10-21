namespace Formulate.Extensions.StoreData.Models
{
    /// <summary>
    /// A single data item used by the <see cref="StoreDataHandler"/>.
    /// </summary>
    public sealed class StoreDataEntry
    {
        /// <summary>
        /// Gets or sets field id.
        /// </summary>
        public Guid FieldId { get; set; }

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
