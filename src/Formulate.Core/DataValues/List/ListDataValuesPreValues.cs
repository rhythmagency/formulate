namespace Formulate.Core.DataValues.List
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// The configuration pre-values used by a <see cref="ListDataValuesDefinition" />.
    /// </summary>
    [DataContract]
    internal sealed class ListDataValuesPreValues
    {
        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        [DataMember(Name = "items")]
        public ListDataValuesPreValuesItem[] Items { get; set; } = Array.Empty<ListDataValuesPreValuesItem>();
    }
}
