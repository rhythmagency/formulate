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
        TPersistedEntity Create<TPersistedEntity>(IPersistedEntity parent) where TPersistedEntity : IPersistedEntity, new();

        /// <summary>
        /// Gets a persisted entity for a given id.
        /// </summary>
        /// <param name="id">The entity ID.</param>
        /// <returns>A <see cref="IPersistedEntity"/>.</returns>
        IPersistedEntity? Get(Guid? id);

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
        /// <param name="filter">Optional filter to use when finding child nodes.</param>
        /// <returns>A <see cref="bool"/>.</returns>
        bool HasChildren(Guid parentId, Func<IPersistedEntity, bool> filter = null);

        /// <summary>
        /// Gets the root items for a given tree root type.
        /// </summary>
        /// <param name="treeRootType">The tree root type.</param>
        /// <returns>A read only collection of <see cref="IPersistedEntity"/> items.</returns>
        IReadOnlyCollection<IPersistedEntity> GetRootItems(TreeRootTypes treeRootType);

        /// <summary>
        /// Gets the root id for a given tree root type.
        /// </summary>
        /// <param name="treeRootType">The tree root type.</param>
        /// <returns>A <see cref="Guid"/>.</returns>
        Guid GetRootId(TreeRootTypes treeRootType);

        /// <summary>
        /// Deletes an entity.
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        IReadOnlyCollection<Guid> Delete(IPersistedEntity entity);

        Guid[] Move(IPersistedEntity entity, Guid[] parentPath);
    }
}