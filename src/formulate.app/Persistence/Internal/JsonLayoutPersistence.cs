namespace formulate.app.Persistence.Internal
{

    // Namespaces.
    using Layouts;
    using Managers;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Web.Hosting;


    /// <summary>
    /// Handles persistence of form layouts to JSON on the file system.
    /// </summary>
    internal class JsonLayoutPersistence : ILayoutPersistence
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
        /// The base path to store layouts in.
        /// </summary>
        private string BasePath
        {
            get
            {
                var basePath = HostingEnvironment.MapPath(Config.JsonBasePath);
                var directory = "Layouts/";
                return Path.Combine(basePath, directory);
            }
        }


        /// <summary>
        /// The file extension used by layout files.
        /// </summary>
        private string Extension
        {
            get
            {
                return ".layout";
            }
        }


        /// <summary>
        /// The wildcard pattern used to find layout files.
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
        public JsonLayoutPersistence(IConfigurationManager configurationManager)
        {
            Config = configurationManager;
        }

        #endregion


        #region Public Methods

        /// <summary>
        /// Persists a layout to the file system.
        /// </summary>
        /// <param name="layout">The layout to persist.</param>
        public void Persist(Layout layout)
        {
            Helper.Persist(layout.Id, layout);
        }


        /// <summary>
        /// Deletes the specified layout.
        /// </summary>
        /// <param name="layoutId">The ID of the layout to delete.</param>
        public void Delete(Guid layoutId)
        {
            Helper.Delete(layoutId);
        }


        /// <summary>
        /// Deletes the layout with the specified alias.
        /// </summary>
        /// <param name="layoutAlias">The alias of the layout to delete.</param>
        public void Delete(string layoutAlias)
        {
            //TODO: ...
            throw new NotImplementedException();
        }


        /// <summary>
        /// Gets the layout with the specified ID.
        /// </summary>
        /// <param name="layoutId">The ID of the layout.</param>
        /// <returns>
        /// The layout.
        /// </returns>
        public Layout Retrieve(Guid layoutId)
        {
            return Helper.Retrieve<Layout>(layoutId);
        }


        /// <summary>
        /// Gets the layout with the specified alias.
        /// </summary>
        /// <param name="layoutAlias">The alias of the layout.</param>
        /// <returns>
        /// The layout.
        /// </returns>
        public Layout Retrieve(string layoutAlias)
        {
            //TODO: ...
            throw new NotImplementedException();
        }


        /// <summary>
        /// Gets all the layouts that are the children of the folder with the specified ID.
        /// </summary>
        /// <param name="parentId">The parent ID.</param>
        /// <returns>
        /// The layouts.
        /// </returns>
        /// <remarks>
        /// You can specify a parent ID of null to get the root layouts.
        /// </remarks>
        public IEnumerable<Layout> RetrieveChildren(Guid? parentId)
        {
            return Helper.RetrieveChildren<Layout>(parentId);
        }

        #endregion

    }

}