using System;
using System.Runtime.Serialization;
using Formulate.Core.Persistence;

namespace Formulate.Core.Layouts
{
    /// <summary>
    /// A persisted layout entity.
    /// </summary>
    [DataContract]
    public sealed class PersistedLayout : PersistedEntity, ILayoutSettings
    {
        /// <summary>
        /// Gets or sets the kind ID.
        /// </summary>
        [DataMember]
        public Guid KindId { get; set; }

        /// <summary>
        /// The ID of the chosen template.
        /// </summary>
        [DataMember]
        public Guid? TemplateId { get; set; }

        /// <summary>
        /// Gets or sets the configuration.
        /// </summary>
        [DataMember]
        public string Data { get; set; }
    }
}
