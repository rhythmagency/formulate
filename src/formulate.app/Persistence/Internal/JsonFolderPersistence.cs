namespace formulate.app.Persistence.Internal
{

    // Namespaces.
    using Folders;
    using Managers;
    using Resolvers;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Web.Hosting;


    /// <summary>
    /// Handles persistence of folders to JSON on the file system.
    /// </summary>
    internal class JsonFolderPersistence : IFolderPersistence
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

                // This needs to be lazy loaded due to the way
                // Umbraco's resolver system works.
                if (helper == null)
                {
                    helper = new JsonPersistenceHelper(
                        BasePath, Extension, WildcardPattern);
                }
                return helper;

            }
        }


        /// <summary>
        /// Configuration manager.
        /// </summary>
        private IConfigurationManager Config
        {
            get
            {
                return Configuration.Current.Manager;
            }
        }


        /// <summary>
        /// The base path to store foldes in.
        /// </summary>
        private string BasePath
        {
            get
            {
                var basePath = HostingEnvironment.MapPath(Config.JsonBasePath);
                var directory = "Folders/";
                return Path.Combine(basePath, directory);
            }
        }


        /// <summary>
        /// The file extension used by folder files.
        /// </summary>
        private string Extension
        {
            get
            {
                return ".folder";
            }
        }


        /// <summary>
        /// The wildcard pattern used to find folder files.
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
        public JsonFolderPersistence()
        {
        }

        #endregion


        #region Public Methods

        /// <summary>
        /// Persists a folder to the file system.
        /// </summary>
        /// <param name="folder">The folder to persist.</param>
        public void Persist(Folder folder)
        {
            Helper.Persist(folder.Id, folder);
        }


        /// <summary>
        /// Gets the folder with the specified ID.
        /// </summary>
        /// <param name="folderId">The ID of the folder.</param>
        /// <returns>
        /// The folder.
        /// </returns>
        public Folder Retrieve(Guid folderId)
        {
            return Helper.Retrieve<Folder>(folderId);
        }


        /// <summary>
        /// Gets all the folders that are the children of the folder
        /// with the specified ID.
        /// </summary>
        /// <param name="parentId">The parent ID.</param>
        /// <returns>
        /// The folders.
        /// </returns>
        public IEnumerable<Folder> RetrieveChildren(Guid? parentId)
        {
            if (parentId == null)
            {
                return new List<Folder>();
            }
            else
            {
                return Helper.RetrieveChildren<Folder>(parentId);
            }
        }

        #endregion

    }

}