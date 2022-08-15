namespace Formulate.Core.DataValues.List
{
    using System.Runtime.Serialization;

    /// <summary>
    /// An item used by the <see cref="ListDataValuesPreValues"/>.
    /// </summary>
    [DataContract]
    internal sealed class ListDataValuesPreValuesItem
    {
        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        [DataMember(Name = "value")]
        public string Value { get; set; }
    }
}
