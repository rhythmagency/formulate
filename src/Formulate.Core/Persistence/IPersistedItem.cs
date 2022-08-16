using System;

namespace Formulate.Core.Persistence
{
    /// <summary>
    /// A contract for creating a persisted item.
    /// </summary>
    public interface IPersistedItem
    {
        /// <summary>
        /// Gets the ID.
        /// </summary>
        Guid Id { get; }

        /// <summary>
        /// Gets the name.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the alias.
        /// </summary>
        string Alias { get; }
    }
}
