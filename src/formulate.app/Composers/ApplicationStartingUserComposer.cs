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
            composition.RegisterUnique<IConfigurationManager, DefaultConfigurationManager>();

            // Initialize Localization Helper.
            composition.RegisterUnique<ILocalizationHelper, LocalizationHelper>();

            // Initialize Definition Helper.
            composition.RegisterUnique<IDefinitionHelper, DefinitionHelper>();

            // Initialize Definition Helper.
            composition.RegisterUnique<IEntityHelper, EntityHelper>();

            // Initialize form persistence resolver.
            composition.RegisterUnique<IFormPersistence, JsonFormPersistence>();

            // Initialize configured form persistence resolver.
            composition.RegisterUnique<IConfiguredFormPersistence, JsonConfiguredFormPersistence>();

            // Initialize layout persistence resolver.
            composition.RegisterUnique<ILayoutPersistence, JsonLayoutPersistence>();

            // Initialize validation persistence resolver.
            composition.RegisterUnique<IValidationPersistence, JsonValidationPersistence>();

            // Initialize data value persistence resolver.
            composition.RegisterUnique<IDataValuePersistence, JsonDataValuePersistence>();

            // Initialize folder persistence resolver.
            composition.RegisterUnique<IFolderPersistence, JsonFolderPersistence>();

            // Initialize entity persistence resolver.
            composition.RegisterUnique<IEntityPersistence, DefaultEntityPersistence>();
        }

        #endregion
    }
}
