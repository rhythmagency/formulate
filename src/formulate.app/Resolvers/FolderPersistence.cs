namespace formulate.app.Resolvers
{

    // Namespaces.
    using Persistence;
    using Umbraco.Core.ObjectResolution;


    /// <summary>
    /// The resolver for the folder persistence manager.
    /// </summary>
    public class FolderPersistence
        : SingleObjectResolverBase<FolderPersistence, IFolderPersistence>
    {

        #region Properties

        /// <summary>
        /// Gets the folder persistence manager.
        /// </summary>
        public IFolderPersistence Manager
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
        /// The folder persistence manager to use.
        /// </param>
        internal FolderPersistence(IFolderPersistence persistence)
            : base(persistence)
        {
        }

        #endregion


        #region Methods

        /// <summary>
        /// Sets the folder persistence manager.
        /// </summary>
        /// <param name="manager">
        /// The new folder persistence manager.
        /// </param>
        public void SetFolderPersistence(IFolderPersistence persistence)
        {
            Value = persistence;
        }

        #endregion

    }

}