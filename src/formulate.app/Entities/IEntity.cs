namespace formulate.app.Entities
{

    // Namespaces.
    using System;


    /// <summary>
    /// An entity (form, folder, layout, etc.).
    /// </summary>
    public interface IEntity
    {

        #region Properties

        /// <summary>
        /// The unique identifier of this entity.
        /// </summary>
        Guid Id { get; set; }


        /// <summary>
        /// The entity path to this entity.
        /// </summary>
        /// <remarks>
        /// This path excludes the root, but includes the entity ID.
        /// </remarks>
        Guid[] Path { get; set; }

        #endregion

    }

}