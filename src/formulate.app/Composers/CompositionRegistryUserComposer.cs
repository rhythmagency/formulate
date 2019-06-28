namespace formulate.app.Composers
{

    // Namespaces.
    using formulate.app.Configuration;
    using formulate.app.Constants.Configuration;
    using formulate.app.DataValues;
    using formulate.app.ExtensionMethods;
    using formulate.app.Forms;
    using formulate.app.Helpers;
    using formulate.app.Persistence;
    using Managers;
    using Persistence.Internal;

    using Umbraco.Core;
    using Umbraco.Core.Composing;

    /// <summary>
    /// Handles registering Formulate components to the Umbraco composition.
    /// </summary>
    [RuntimeLevel(MinLevel = RuntimeLevel.Install)]
    public sealed class CompositionRegistryUserComposer : IUserComposer
    {
        #region Methods

        /// <summary>
        /// Registers compoents.
        /// </summary>
        /// <param name="composition">
        /// The composition.
        /// </param>
        public void Compose(Composition composition)
        {
            // Register Configuration
            composition.Configs.AddJsonConfig<IFormulateConfig, FormulateConfig>(ConfigFilePaths.FormulateConfigPath);

            // Register Config Manager
            composition.RegisterUnique<IConfigurationManager, DefaultConfigurationManager>();

            // Initialize Localization Helper.
            composition.RegisterUnique<ILocalizationHelper, LocalizationHelper>();

            var formFieldTypes = composition.TypeLoader.GetTypes<IFormFieldType>();
            var formHandlerType = composition.TypeLoader.GetTypes<IFormHandlerType>();
            var dataValueKinds = composition.TypeLoader.GetTypes<IDataValueKind>();

            composition.FormFieldTypes().Add(() => formFieldTypes);
            composition.FormHandlerTypes().Add(() => formHandlerType);
            composition.DataValueKinds().Add(() => dataValueKinds);

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
