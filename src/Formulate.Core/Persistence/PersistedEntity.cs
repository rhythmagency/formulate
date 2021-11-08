using System;

namespace Formulate.Core.Persistence
{
    /// <summary>
    /// A base entity for all other persisted entities.
    /// </summary>
    public abstract class PersistedEntity : IPersistedEntity
    {
        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the path.
        /// </summary>
        public Guid[] Path { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }
    }
}
