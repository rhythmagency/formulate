namespace Formulate.Core.DataValues.PairList
{
    using System.Runtime.Serialization;

    /// <summary>
    /// An item used by the <see cref="PairListDataValuesPreValues"/>.
    /// </summary>
    [DataContract]
    internal sealed class PairListDataValuesPreValuesItem
    {
        /// <summary>
        /// Gets or sets the secondary value.
        /// </summary>
        [DataMember(Name = "secondary")]
        public string Secondary { get; set; }

        /// <summary>
        /// Gets or sets the primary value.
        /// </summary>
        [DataMember(Name = "primary")]
        public string Primary { get; set; }
    }
}
