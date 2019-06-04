namespace formulate.app.Composers
{

    // Namespaces.
    using formulate.app.Helpers;
    using formulate.app.Persistence;
    using Managers;
    using Persistence.Internal;

    using Umbraco.Core;
    using Umbraco.Core.Composing;

    /// <summary>
    /// Handles the application starting event.
    /// </summary>
    [RuntimeLevel(MinLevel = RuntimeLevel.Boot)]
    public sealed class ApplicationStartingUserComposer : IUserComposer
    {
        #region Methods

        /// <summary>
        /// Registers resolvers.
        /// </summary>
        /// <param name="composition">
        /// The composition.
        /// </param>
        public void Compose(Composition composition)
        {
            // Register Config Manager
            composition.Register<IConfigurationManager, DefaultConfigurationManager>(Lifetime.Singleton);

            // Initialize Localization Helper.
            composition.Register<ILocalizationHelper, LocalizationHelper>(Lifetime.Singleton);

            // Initialize Definition Helper.
            composition.Register<IDefinitionHelper, DefinitionHelper>(Lifetime.Singleton);

            // Initialize Definition Helper.
            composition.Register<IEntityHelper, EntityHelper>(Lifetime.Singleton);

            // Initialize form persistence resolver.
            composition.Register<IFormPersistence, JsonFormPersistence>(Lifetime.Singleton);

            // Initialize configured form persistence resolver.
            composition.Register<IConfiguredFormPersistence, JsonConfiguredFormPersistence>(Lifetime.Singleton);
            
            // Initialize layout persistence resolver.
            composition.Register<ILayoutPersistence, JsonLayoutPersistence>(Lifetime.Singleton);

            // Initialize validation persistence resolver.
            composition.Register<IValidationPersistence, JsonValidationPersistence>(Lifetime.Singleton);

            // Initialize data value persistence resolver.
            composition.Register<IDataValuePersistence, JsonDataValuePersistence>(Lifetime.Singleton);

            // Initialize folder persistence resolver.
            composition.Register<IFolderPersistence, JsonFolderPersistence>(Lifetime.Singleton);

            // Initialize entity persistence resolver.
            composition.Register<IEntityPersistence, DefaultEntityPersistence>(Lifetime.Singleton);
        }

        #endregion
    }
}
