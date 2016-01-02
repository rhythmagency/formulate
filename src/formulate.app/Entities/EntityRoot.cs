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


        /// <summary>
        /// The name of this entity.
        /// </summary>
        public string Name { get; set; }


        /// <summary>
        /// The icon for this root entity.
        /// </summary>
        public string Icon { get; set; }


        /// <summary>
        /// The kind of this entity.
        /// </summary>
        public EntityKind Kind
        {
            get
            {
                return EntityKind.Root;
            }
        }

        #endregion

    }

}