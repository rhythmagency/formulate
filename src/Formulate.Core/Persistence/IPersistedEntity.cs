using System;

namespace Formulate.Core.Persistence
{
    /// <summary>
    /// A contract for creating a persisted entity.
    /// </summary>
    public interface IPersistedEntity
    {
        /// <summary>
        /// Gets the ID.
        /// </summary>
        Guid Id { get; }

        /// <summary>
        /// Gets the path.
        /// </summary>
        Guid[] Path { get; }

        /// <summary>
        /// Gets the name.
        /// </summary>
        string Name { get; }
    }
}
