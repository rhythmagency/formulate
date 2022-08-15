namespace Formulate.Core.DataValues.PairList
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// The configuration pre-values used by a <see cref="PairListDataValuesDefinition" />.
    /// </summary>
    [DataContract]
    internal sealed class PairListDataValuesPreValues
    {
        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        [DataMember(Name = "items")]
        public PairListDataValuesPreValuesItem[] Items { get; set; } = Array.Empty<PairListDataValuesPreValuesItem>(); 
    }
}
