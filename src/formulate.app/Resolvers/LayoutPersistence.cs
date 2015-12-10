namespace formulate.app.Resolvers
{

    // Namespaces.
    using Persistence;
    using Umbraco.Core.ObjectResolution;


    /// <summary>
    /// The resolver for the layout persistence manager.
    /// </summary>
    public class LayoutPersistence
        : SingleObjectResolverBase<LayoutPersistence, ILayoutPersistence>
    {

        #region Properties

        /// <summary>
        /// Gets the layout persistence manager.
        /// </summary>
        public ILayoutPersistence Manager
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
        /// The layout persistence manager to use.
        /// </param>
        internal LayoutPersistence(ILayoutPersistence persistence)
            : base(persistence)
        {
        }

        #endregion


        #region Methods

        /// <summary>
        /// Sets the layout persistence manager.
        /// </summary>
        /// <param name="manager">
        /// The new layout persistence manager.
        /// </param>
        public void SetLayoutPersistence(ILayoutPersistence persistence)
        {
            Value = persistence;
        }

        #endregion

    }

}