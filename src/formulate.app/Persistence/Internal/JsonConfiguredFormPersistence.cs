namespace formulate.app.Persistence.Internal
{

    // Namespaces.
    using Forms;
    using Managers;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Web.Hosting;


    /// <summary>
    /// Handles persistence of configured forms to JSON on the file system.
    /// </summary>
    internal class JsonConfiguredFormPersistence : IConfiguredFormPersistence
    {

        #region Variables

        private JsonPersistenceHelper helper = null;

        #endregion


        #region Properties

        /// <summary>
        /// A helper for JSON operations.
        /// </summary>
        private JsonPersistenceHelper Helper
        {
            get
            {

                // This needs to be lazy loaded due to the way Umbraco's resolver system works.
                if (helper == null)
                {
                    helper = new JsonPersistenceHelper(BasePath, Extension, WildcardPattern);
                }
                return helper;

            }
        }


        /// <summary>
        /// Configuration manager.
        /// </summary>
        private IConfigurationManager Config { get; set; }


        /// <summary>
        /// The base path to store configured forms in.
        /// </summary>
        private string BasePath
        {
            get
            {
                var basePath = HostingEnvironment.MapPath(Config.JsonBasePath);
                var directory = "ConfiguredForms/";
                return Path.Combine(basePath, directory);
            }
        }


        /// <summary>
        /// The file extension used by configured form files.
        /// </summary>
        private string Extension
        {
            get
            {
                return ".conform";
            }
        }


        /// <summary>
        /// The wildcard pattern used to find configured form files.
        /// </summary>
        private string WildcardPattern
        {
            get
            {
                return "*" + Extension;
            }
        }

        #endregion


        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public JsonConfiguredFormPersistence(IConfigurationManager configurationManager)
        {
            Config = configurationManager;
        }

        #endregion


        #region Public Methods

        /// <summary>
        /// Persists a configured form to the file system.
        /// </summary>
        /// <param name="configuredForm">The configured form to persist.</param>
        public void Persist(ConfiguredForm configuredForm)
        {
            Helper.Persist(configuredForm.Id, configuredForm);
        }


        /// <summary>
        /// Deletes the specified configured form.
        /// </summary>
        /// <param name="configuredFormId">The ID of the configured form to delete.</param>
        public void Delete(Guid configuredFormId)
        {
            Helper.Delete(configuredFormId);
        }


        /// <summary>
        /// Deletes the configured form with the specified alias.
        /// </summary>
        /// <param name="configuredFormAlias">The alias of the configured form to delete.</param>
        public void Delete(string configuredFormAlias)
        {
            //TODO: ...
            throw new NotImplementedException();
        }


        /// <summary>
        /// Gets the configured form with the specified ID.
        /// </summary>
        /// <param name="configuredFormId">The ID of the configured form.</param>
        /// <returns>
        /// The configured form.
        /// </returns>
        public ConfiguredForm Retrieve(Guid configuredFormId)
        {
            return Helper.Retrieve<ConfiguredForm>(configuredFormId);
        }


        /// <summary>
        /// Gets the configured form with the specified alias.
        /// </summary>
        /// <param name="configuredFormAlias">The alias of the configured form.</param>
        /// <returns>
        /// The configured form.
        /// </returns>
        public ConfiguredForm Retrieve(string configuredFormAlias)
        {
            //TODO: ...
            throw new NotImplementedException();
        }


        /// <summary>
        /// Gets all the configured forms that are the children of the folder with the specified ID.
        /// </summary>
        /// <param name="parentId">The parent ID.</param>
        /// <returns>
        /// The configured forms.
        /// </returns>
        public IEnumerable<ConfiguredForm> RetrieveChildren(Guid parentId)
        {
            return Helper.RetrieveChildren<ConfiguredForm>(parentId);
        }

        #endregion

    }

}