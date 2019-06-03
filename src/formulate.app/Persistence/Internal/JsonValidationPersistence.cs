namespace formulate.app.Persistence.Internal
{

    // Namespaces.
    using Managers;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Web.Hosting;
    using Validations;


    /// <summary>
    /// Handles persistence of form validations to JSON on the file system.
    /// </summary>
    internal class JsonValidationPersistence : IValidationPersistence
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

                // This needs to be lazy loaded due to the way Umbraco's
                // resolver system works.
                if (helper == null)
                {
                    helper = new JsonPersistenceHelper(BasePath,
                        Extension, WildcardPattern);
                }
                return helper;

            }
        }


        /// <summary>
        /// Configuration manager.
        /// </summary>
        private IConfigurationManager Config { get; set; }


        /// <summary>
        /// The base path to store validations in.
        /// </summary>
        private string BasePath
        {
            get
            {
                var basePath = HostingEnvironment.MapPath(Config.JsonBasePath);
                var directory = "Validations/";
                return Path.Combine(basePath, directory);
            }
        }


        /// <summary>
        /// The file extension used by validation files.
        /// </summary>
        private string Extension
        {
            get
            {
                return ".validation";
            }
        }


        /// <summary>
        /// The wildcard pattern used to find validation files.
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
        public JsonValidationPersistence(IConfigurationManager configurationManager)
        {
            Config = configurationManager;
        }

        #endregion


        #region Public Methods

        /// <summary>
        /// Persists a validation to the file system.
        /// </summary>
        /// <param name="validation">The validation to persist.</param>
        public void Persist(Validation validation)
        {
            Helper.Persist(validation.Id, validation);
        }


        /// <summary>
        /// Deletes the specified validation.
        /// </summary>
        /// <param name="validationId">
        /// The ID of the validation to delete.
        /// </param>
        public void Delete(Guid validationId)
        {
            Helper.Delete(validationId);
        }


        /// <summary>
        /// Deletes the validation with the specified alias.
        /// </summary>
        /// <param name="validationAlias">
        /// The alias of the validation to delete.
        /// </param>
        public void Delete(string validationAlias)
        {
            //TODO: ...
            throw new NotImplementedException();
        }


        /// <summary>
        /// Gets the validation with the specified ID.
        /// </summary>
        /// <param name="validationId">The ID of the validation.</param>
        /// <returns>
        /// The validation.
        /// </returns>
        public Validation Retrieve(Guid validationId)
        {
            return Helper.Retrieve<Validation>(validationId);
        }


        /// <summary>
        /// Gets the validation with the specified alias.
        /// </summary>
        /// <param name="validationAlias">The alias of the validation.</param>
        /// <returns>
        /// The validation.
        /// </returns>
        public Validation Retrieve(string validationAlias)
        {
            //TODO: ...
            throw new NotImplementedException();
        }


        /// <summary>
        /// Gets all the validations that are the children of the
        /// folder with the specified ID.
        /// </summary>
        /// <param name="parentId">The parent ID.</param>
        /// <returns>
        /// The validations.
        /// </returns>
        /// <remarks>
        /// You can specify a parent ID of null to get the root validations.
        /// </remarks>
        public IEnumerable<Validation> RetrieveChildren(Guid? parentId)
        {
            return Helper.RetrieveChildren<Validation>(parentId);
        }

        #endregion

    }

}