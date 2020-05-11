namespace formulate.app.Resolvers
{

    // Namespaces.
    using Managers;
    using Umbraco.Core.ObjectResolution;


    /// <summary>
    /// The resolver for the configuration manager.
    /// </summary>
    public class Configuration
        : SingleObjectResolverBase<Configuration, IConfigurationManager>
    {

        #region Properties

        /// <summary>
        /// Gets the configuration manager.
        /// </summary>
        public IConfigurationManager Manager
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
        /// The configuration manager to use.
        /// </param>
        public Configuration(IConfigurationManager manager)
            : base(manager)
        {
        }

        #endregion


        #region Methods

        /// <summary>
        /// Sets the configuration manager.
        /// </summary>
        /// <param name="manager">
        /// The new configuration manager.
        /// </param>
        public void SetConfigurationManager(IConfigurationManager manager)
        {
            Value = manager;
        }

        #endregion

    }

}