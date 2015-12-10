namespace formulate.app.Persistence.Internal
{

    // Namespaces.
    using Helpers;
    using Layouts;
    using Managers;
    using Resolvers;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Web.Hosting;


    /// <summary>
    /// Handles persistence of form layouts to JSON on the file system.
    /// </summary>
    internal class JsonLayoutPersistence : ILayoutPersistence
    {

        #region Properties

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
        public JsonLayoutPersistence()
        {
        }

        #endregion


        #region Public Methods

        /// <summary>
        /// Persists a layout to the file system.
        /// </summary>
        /// <param name="layout">The layout to persist.</param>
        public void Persist(Layout layout)
        {
            var path = GetLayoutPath(layout.Id);
            var serialized = JsonHelper.Serialize(layout);
            EnsurePathExists(BasePath);
            WriteFile(path, serialized);
        }


        /// <summary>
        /// Deletes the specified layout.
        /// </summary>
        /// <param name="layoutId">The ID of the layout to delete.</param>
        public void Delete(Guid layoutId)
        {
            var path = GetLayoutPath(layoutId);
            if (File.Exists(path))
            {
                File.Delete(path);
            }
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
            var path = GetLayoutPath(layoutId);
            var json = GetFileContents(path);
            var layout = JsonHelper.Deserialize<Layout>(json);
            return layout;
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
            if (parentId.HasValue)
            {
                //TODO: ...
                return new List<Layout>();
            }
            else
            {
                //TODO: For now, I am getting all layouts. Once there are folders, get only root items.
                EnsurePathExists(BasePath);
                var files = Directory.GetFiles(BasePath, WildcardPattern);
                var layouts = new List<Layout>();
                foreach (var file in files)
                {
                    var contents = GetFileContents(file);
                    var layout = JsonHelper.Deserialize<Layout>(contents);
                    layouts.Add(layout);
                }
                return layouts;
            }
        }

        #endregion


        #region Private Methods

        /// <summary>
        /// Writes the specified file at the specified path.
        /// </summary>
        /// <param name="path">The path of the file.</param>
        /// <param name="contents">The contents of the file.</param>
        private void WriteFile(string path, string contents)
        {
            File.WriteAllText(path, contents);
        }


        /// <summary>
        /// Gets the contents of the file at the specified path.
        /// </summary>
        /// <param name="path">The path to the file.</param>
        /// <returns>
        /// The file contents, or null.
        /// </returns>
        private string GetFileContents(string path)
        {
            if (File.Exists(path))
            {
                return File.ReadAllText(path);
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// Gets the file path to the layout with the specified ID.
        /// </summary>
        /// <param name="layoutId">The layout's ID.</param>
        /// <returns>The file to the layout's file.</returns>
        private string GetLayoutPath(Guid layoutId)
        {
            var id = GuidHelper.GetString(layoutId);
            var path = Path.Combine(BasePath, id + Extension);
            return path;
        }


        /// <summary>
        /// Ensures that the specified path exists.
        /// </summary>
        /// <param name="path">The path.</param>
        private void EnsurePathExists(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        #endregion

    }

}