using System;
using System.Runtime.Serialization;

namespace Formulate.Core.Persistence
{
    /// <summary>
    /// A base entity for all other persisted entities.
    /// </summary>
    [DataContract]
    public abstract class PersistedEntity : IPersistedEntity
    {
        /// <inheritdoc cref="IPersistedEntity.Id"/>
        [DataMember]
        public Guid Id { get; set; }

        /// <inheritdoc cref="IPersistedEntity.Path"/>
        [DataMember]
        public Guid[] Path { get; set; }

        /// <inheritdoc cref="IPersistedEntity.Name"/>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the alias.
        /// </summary>
        [DataMember]
        public string Alias { get; set; }
    }
}
