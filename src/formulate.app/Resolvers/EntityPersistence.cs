namespace formulate.app.Resolvers
{

    // Namespaces.
    using Persistence;
    using Umbraco.Core.ObjectResolution;


    /// <summary>
    /// The resolver for the entity persistence manager.
    /// </summary>
    public class EntityPersistence
        : SingleObjectResolverBase<EntityPersistence, IEntityPersistence>
    {

        #region Properties

        /// <summary>
        /// Gets the entity persistence manager.
        /// </summary>
        public IEntityPersistence Manager
        {
            get
            {
                return Value;
            }
        }

        #endregion


        #region Constructors

        /// <summary>
        /// Primary constructor.
        /// </summary>
        /// <param name="manager">
        /// The entity persistence manager to use.
        /// </param>
        internal EntityPersistence(IEntityPersistence persistence)
            : base(persistence)
        {
        }

        #endregion


        #region Methods

        /// <summary>
        /// Sets the entity persistence manager.
        /// </summary>
        /// <param name="manager">
        /// The new entity persistence manager.
        /// </param>
        public void SetEntityPersistence(IEntityPersistence persistence)
        {
            Value = persistence;
        }

        #endregion

    }

}