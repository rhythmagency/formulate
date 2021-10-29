using System;
using System.Collections.Generic;
using Formulate.Core.Persistence;

namespace Formulate.BackOffice.Persistence
{
    /// <summary>
    /// A contract for creating a tree entity repository.
    /// </summary>
    /// <remarks>A tree entity repository pulls entities from all repositories.</remarks>
    public interface ITreeEntityRepository
    {
        /// <summary>
        /// Gets a persisted entity for a given id.
        /// </summary>
        /// <param name="id">The entity ID.</param>
        /// <returns>A <see cref="IPersistedEntity"/>.</returns>
        IPersistedEntity Get(Guid id);

        /// <summary>
        /// Gets a the children a given ID.
        /// </summary>
        /// <param name="parentId">The parent ID.</param>
        /// <returns>A read only collection of <see cref="IPersistedEntity"/> items.</returns>
        IReadOnlyCollection<IPersistedEntity> GetChildren(Guid parentId);

        /// <summary>
        /// Checks if a the entity ID has an children.
        /// </summary>
        /// <param name="parentId">The parent ID.</param>
        /// <returns>A <see cref="bool"/>.</returns>
        bool HasChildren(Guid parentId);

        /// <summary>
        /// Gets the root items for a given tree root type.
        /// </summary>
        /// <param name="treeRootType">The tree root type.</param>
        /// <returns>A read only collection of <see cref="IPersistedEntity"/> items.</returns>
        IReadOnlyCollection<IPersistedEntity> GetRootItems(TreeRootTypes treeRootType);
    }
}