namespace formulate.app.Handlers
{

    // Namespaces.
    using Managers;
    using Persistence.Internal;
    using Resolvers;
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


            // Initialize form persistence resolver.
            var formPersistence = new JsonFormPersistence();
            FormPersistence.Current = new FormPersistence(formPersistence);


            // Initialize configured form persistence resolver.
            var conFormPersistence = new JsonConfiguredFormPersistence();
            ConfiguredFormPersistence.Current =
                new ConfiguredFormPersistence(conFormPersistence);


            // Initialize layout persistence resolver.
            var layoutPersistence = new JsonLayoutPersistence();
            LayoutPersistence.Current =
                new LayoutPersistence(layoutPersistence);


            // Initialize validation persistence resolver.
            var validationPersistence = new JsonValidationPersistence();
            ValidationPersistence.Current =
                new ValidationPersistence(validationPersistence);


            // Initialize data value persistence resolver.
            var dataValuePersistence = new JsonDataValuePersistence();
            DataValuePersistence.Current =
                new DataValuePersistence(dataValuePersistence);


            // Initialize folder persistence resolver.
            var folderPersistence = new JsonFolderPersistence();
            FolderPersistence.Current =
                new FolderPersistence(folderPersistence);


            // Initialize entity persistence resolver.
            var entityPersistence = new DefaultEntityPersistence();
            EntityPersistence.Current =
                new EntityPersistence(entityPersistence);

        }

        #endregion

    }

}