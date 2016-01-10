namespace formulate.app.Resolvers
{

    // Namespaces.
    using Persistence;
    using Umbraco.Core.ObjectResolution;


    /// <summary>
    /// The resolver for the data value persistence manager.
    /// </summary>
    public class DataValuePersistence
        : SingleObjectResolverBase<DataValuePersistence, IDataValuePersistence>
    {

        #region Properties

        /// <summary>
        /// Gets the data value persistence manager.
        /// </summary>
        public IDataValuePersistence Manager
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
        /// The data value persistence manager to use.
        /// </param>
        internal DataValuePersistence(IDataValuePersistence persistence)
            : base(persistence)
        {
        }

        #endregion


        #region Methods

        /// <summary>
        /// Sets the data value persistence manager.
        /// </summary>
        /// <param name="manager">
        /// The new data value persistence manager.
        /// </param>
        public void SetDataValuePersistence(IDataValuePersistence persistence)
        {
            Value = persistence;
        }

        #endregion

    }

}