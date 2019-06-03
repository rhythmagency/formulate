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
    /// Handles persistence of forms to JSON on the file system.
    /// </summary>
    internal class JsonFormPersistence : IFormPersistence
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
        /// The base path to store forms in.
        /// </summary>
        private string BasePath
        {
            get
            {
                var basePath = HostingEnvironment.MapPath(Config.JsonBasePath);
                var directory = "Forms/";
                return Path.Combine(basePath, directory);
            }
        }


        /// <summary>
        /// The file extension used by form files.
        /// </summary>
        private string Extension
        {
            get
            {
                return ".form";
            }
        }


        /// <summary>
        /// The wildcard pattern used to find form files.
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
        /// <param name="configurationManager">
        /// The Configuration Manager.
        /// </param>
        public JsonFormPersistence(IConfigurationManager configurationManager)
        {
            Config = configurationManager;
        }

        #endregion


        #region Public Methods

        /// <summary>
        /// Persists a form to the file system.
        /// </summary>
        /// <param name="form">The form to persist.</param>
        public void Persist(Form form)
        {
            Helper.Persist(form.Id, form);
        }


        /// <summary>
        /// Deletes the specified form.
        /// </summary>
        /// <param name="formId">The ID of the form to delete.</param>
        public void Delete(Guid formId)
        {
            Helper.Delete(formId);
        }


        /// <summary>
        /// Deletes the form with the specified alias.
        /// </summary>
        /// <param name="formAlias">The alias of the form to delete.</param>
        public void Delete(string formAlias)
        {
            //TODO: ...
            throw new NotImplementedException();
        }


        /// <summary>
        /// Gets the form with the specified ID.
        /// </summary>
        /// <param name="formId">The ID of the form.</param>
        /// <returns>
        /// The form.
        /// </returns>
        public Form Retrieve(Guid formId)
        {
            return Helper.Retrieve<Form>(formId);
        }


        /// <summary>
        /// Gets the form with the specified alias.
        /// </summary>
        /// <param name="formAlias">The alias of the form.</param>
        /// <returns>
        /// The form.
        /// </returns>
        public Form Retrieve(string formAlias)
        {
            //TODO: ...
            throw new NotImplementedException();
        }


        /// <summary>
        /// Gets all the forms that are the children of the folder
        /// with the specified ID.
        /// </summary>
        /// <param name="parentId">The parent ID.</param>
        /// <returns>
        /// The forms.
        /// </returns>
        /// <remarks>
        /// You can specify a parent ID of null to get the root forms.
        /// </remarks>
        public IEnumerable<Form> RetrieveChildren(Guid? parentId)
        {
            return Helper.RetrieveChildren<Form>(parentId);
        }

        #endregion

    }

}