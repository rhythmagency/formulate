namespace formulate.app.Persistence
{

    // Namespaces.
    using Entities;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Interface for persistence of Entities.
    /// </summary>
    public interface IEntityPersistence
    {
        /// <summary>
        /// Retrieve an Entity by ID.
        /// </summary>
        /// <param name="id">
        /// The id of the Entity.
        /// </param>
        /// <returns>
        /// A <see cref="IEntity"/>.
        /// </returns>
        IEntity Retrieve(Guid id);

        /// <summary>
        /// Retrieve children by their parent ID.
        /// </summary>
        /// <param name="parentId">
        /// The parent id.
        /// </param>
        /// <returns>
        /// If found, a collection of <see cref="IEntity"/>.
        /// </returns>
        IEnumerable<IEntity> RetrieveChildren(Guid? parentId);

        /// <summary>
        /// Retrieve descendants by their parent ID.
        /// </summary>
        /// <param name="parentId">
        /// The parent id.
        /// </param>
        /// <returns>
        /// If found, a collection of <see cref="IEntity"/>.
        /// </returns>
        IEnumerable<IEntity> RetrieveDescendants(Guid parentId);

        /// <summary>
        /// Move an Entity to a new parent path.
        /// </summary>
        /// <param name="entity">
        /// The Entity.
        /// </param>
        /// <param name="parentPath">
        /// The new parent path.
        /// </param>
        /// <returns>
        /// The Entity's new path.
        /// </returns>
        Guid[] MoveEntity(IEntity entity, Guid[] parentPath);

        /// <summary>
        /// Delete an Entity.
        /// </summary>
        /// <param name="entity">
        /// The Entity.
        /// </param>
        void DeleteEntity(IEntity entity);
    }
}
