namespace formulate.app.Resolvers
{

    // Namespaces.
    using Persistence;
    using Umbraco.Core.ObjectResolution;


    /// <summary>
    /// The resolver for the configured form persistence manager.
    /// </summary>
    public class ConfiguredFormPersistence
        : SingleObjectResolverBase<ConfiguredFormPersistence, IConfiguredFormPersistence>
    {

        #region Properties

        /// <summary>
        /// Gets the configured form persistence manager.
        /// </summary>
        public IConfiguredFormPersistence Manager
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
        /// The configured form persistence manager to use.
        /// </param>
        internal ConfiguredFormPersistence(IConfiguredFormPersistence persistence)
            : base(persistence)
        {
        }

        #endregion


        #region Methods

        /// <summary>
        /// Sets the configured form persistence manager.
        /// </summary>
        /// <param name="manager">
        /// The new configured form persistence manager.
        /// </param>
        public void SetConfiguredFormPersistence(IConfiguredFormPersistence persistence)
        {
            Value = persistence;
        }

        #endregion

    }

}