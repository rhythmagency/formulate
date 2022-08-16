using System;

namespace Formulate.Core.Persistence
{
    /// <summary>
    /// A contract for creating a persisted entity.
    /// </summary>
    public interface IPersistedEntity : IPersistedItem
    {
        /// <summary>
        /// Gets the path.
        /// </summary>
        Guid[] Path { get; }
    }
}
