using System;

namespace Formulate.Core.Persistence
{
    /// <summary>
    /// A base entity for all other persisted entities.
    /// </summary>
    public abstract class PersistedEntity : IPersistedEntity
    {
        /// <inheritdoc cref="IPersistedEntity.Id"/>
        public Guid Id { get; set; }

        /// <inheritdoc cref="IPersistedEntity.Path"/>
        public Guid[] Path { get; set; }

        /// <inheritdoc cref="IPersistedEntity.Name"/>
        public string Name { get; set; }
    }
}
