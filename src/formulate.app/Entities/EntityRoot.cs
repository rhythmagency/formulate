namespace formulate.app.Entities
{

    // Namespaces.
    using System;


    /// <summary>
    /// A root-level entity (e.g., "Forms", "Layouts").
    /// </summary>
    public class EntityRoot : IEntity
    {

        #region Properties

        /// <summary>
        /// The unique ID of this entity.
        /// </summary>
        public Guid Id { get; set; }


        /// <summary>
        /// The entity path to this entity.
        /// </summary>
        /// <remarks>
        /// This path will only contain one item (the ID of this entity).
        /// </remarks>
        public Guid[] Path { get; set; }

        #endregion

    }

}