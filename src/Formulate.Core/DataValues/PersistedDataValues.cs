using System;
using System.Runtime.Serialization;
using Formulate.Core.Persistence;

namespace Formulate.Core.DataValues
{
    /// <summary>
    /// A persisted data values entity.
    /// </summary>
    [DataContract]
    public sealed class PersistedDataValues : PersistedEntity, IDataValuesSettings
    {
        /// <summary>
        /// Gets or sets the kind ID.
        /// </summary>
        [DataMember]
        public Guid KindId { get; set; }

        /// <summary>
        /// Gets or sets the configuration.
        /// </summary>
        [DataMember]
        public string Data { get; set; }

        /// <summary>
        /// Gets or sets the alias of this data value.
        /// </summary>
        [DataMember]
        public string Alias { get; set; }
    }
}