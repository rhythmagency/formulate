namespace formulate.app.Handlers
{

    // Namespaces.
    using Managers;
    using Umbraco.Core;
    using ConfigResolver = formulate.app.Resolvers.Configuration;


    /// <summary>
    /// Handles the application starting event.
    /// </summary>
    public class ApplicationStartingHandler : ApplicationEventHandler
    {

        #region Methods

        /// <summary>
        /// Application starting.
        /// </summary>
        protected override void ApplicationStarting(
            UmbracoApplicationBase umbracoApplication,
            ApplicationContext applicationContext)
        {
            InitializeResolvers();
        }

        /// <summary>
        /// Initializes resolvers.
        /// </summary>
        private void InitializeResolvers()
        {

            // Initialize configuration resolver.
            var configManager = new DefaultConfigurationManager();
            ConfigResolver.Current = new ConfigResolver(configManager);

        }

        #endregion

    }

}