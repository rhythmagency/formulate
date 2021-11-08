using System;
using System.Collections.Generic;

namespace Formulate.Core.Persistence
{
    /// <summary>
    /// The underlying contract for creating an entity repository class.
    /// </summary>
    /// <typeparam name="TPersistedEntity">The type of entity to manage.</typeparam>
    public interface IEntityRepository<TPersistedEntity> where TPersistedEntity : class, IPersistedEntity
    {
        /// <summary>
        /// Saves or creates an entity.
        /// </summary>
        /// <param name="entity">The entity to persist.</param>
        /// <returns>
        /// A <typeparamref name="TPersistedEntity"/>.
        /// </returns>
        TPersistedEntity Save(TPersistedEntity entity);

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entityId">The ID of the entity to delete.</param>
        void Delete(Guid entityId);

        /// <summary>
        /// Gets the entity with the specified ID.
        /// </summary>
        /// <param name="entityId">The ID of the entity.</param>
        /// <returns>
        /// A <typeparamref name="TPersistedEntity"/>.
        /// </returns>
        TPersistedEntity Get(Guid entityId);

        /// <summary>
        /// Gets all the entities that are the children of the folder with the specified ID.
        /// </summary>
        /// <param name="parentId">The parent ID.</param>
        /// <returns>
        /// A read only collection of <typeparamref name="TPersistedEntity"/>.
        /// </returns>
        IReadOnlyCollection<TPersistedEntity> GetChildren(Guid parentId);

        /// <summary>
        /// Moves the entity to a new location and returns its path.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="newPath">The new location.</param>
        /// <returns></returns>
        Guid[] Move(TPersistedEntity entity, Guid[] newPath);

        /// <summary>
        /// Gets any root level items.
        /// </summary>
        /// <returns>
        /// A read only collection of <typeparamref name="TPersistedEntity"/>.
        /// </returns>
        IReadOnlyCollection<TPersistedEntity> GetRootItems();
    }
}
