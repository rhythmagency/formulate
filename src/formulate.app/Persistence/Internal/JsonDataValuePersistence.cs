namespace formulate.app.Persistence.Internal
{

    // Namespaces.
    using DataValues;
    using Managers;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Web.Hosting;


    /// <summary>
    /// Handles persistence of data values to JSON on the file system.
    /// </summary>
    internal class JsonDataValuePersistence : IDataValuePersistence
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
        /// The base path to store data values in.
        /// </summary>
        private string BasePath
        {
            get
            {
                var basePath = HostingEnvironment.MapPath(Config.JsonBasePath);
                var directory = "DataValues/";
                return Path.Combine(basePath, directory);
            }
        }


        /// <summary>
        /// The file extension used by data value files.
        /// </summary>
        private string Extension
        {
            get
            {
                return ".dataValue";
            }
        }


        /// <summary>
        /// The wildcard pattern used to find data value files.
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
        public JsonDataValuePersistence(IConfigurationManager configurationManager)
        {
            Config = configurationManager;
        }

        #endregion


        #region Public Methods

        /// <summary>
        /// Persists a data value to the file system.
        /// </summary>
        /// <param name="dataValue">The data value to persist.</param>
        public void Persist(DataValue dataValue)
        {
            Helper.Persist(dataValue.Id, dataValue);
        }


        /// <summary>
        /// Deletes the specified data value.
        /// </summary>
        /// <param name="dataValueId">The ID of the data value to delete.</param>
        public void Delete(Guid dataValueId)
        {
            Helper.Delete(dataValueId);
        }


        /// <summary>
        /// Deletes the data value with the specified alias.
        /// </summary>
        /// <param name="dataValueAlias">
        /// The alias of the data value to delete.
        /// </param>
        public void Delete(string dataValueAlias)
        {
            //TODO: ...
            throw new NotImplementedException();
        }


        /// <summary>
        /// Gets the data value with the specified ID.
        /// </summary>
        /// <param name="dataValueId">
        /// The ID of the data value.
        /// </param>
        /// <returns>
        /// The data value.
        /// </returns>
        public DataValue Retrieve(Guid dataValueId)
        {
            return Helper.Retrieve<DataValue>(dataValueId);
        }


        /// <summary>
        /// Gets the data value with the specified alias.
        /// </summary>
        /// <param name="dataValueAlias">The alias of the data value.</param>
        /// <returns>
        /// The data value.
        /// </returns>
        public DataValue Retrieve(string dataValueAlias)
        {
            //TODO: ...
            throw new NotImplementedException();
        }


        /// <summary>
        /// Gets all the data values that are the children of the
        /// folder with the specified ID.
        /// </summary>
        /// <param name="parentId">The parent ID.</param>
        /// <returns>
        /// The data values.
        /// </returns>
        /// <remarks>
        /// You can specify a parent ID of null to get the root data values.
        /// </remarks>
        public IEnumerable<DataValue> RetrieveChildren(Guid? parentId)
        {
            return Helper.RetrieveChildren<DataValue>(parentId);
        }

        #endregion

    }

}