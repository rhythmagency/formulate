namespace Formulate.Core.FormHandlers
{
    using Formulate.Core.Persistence;
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// A persisted form handler.
    /// </summary>
    [DataContract]
    public sealed class PersistedFormHandler : IFormHandlerSettings, IPersistedItem
    {
        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        [DataMember]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the kind ID.
        /// </summary>
        [DataMember(Name = "TypeId")]
        public Guid KindId { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the configuration.
        /// </summary>
        [DataMember(Name = "HandlerConfiguration")]
        public string Data { get; set; }

        /// <summary>
        /// Gets or sets the alias.
        /// </summary>
        [DataMember]
        public string Alias { get; set; }

        /// <summary>
        /// Gets or sets value indicating whether this is enabled.
        /// </summary>
        [DataMember]
        public bool Enabled { get; set; }
    }
}
