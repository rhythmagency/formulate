namespace formulate.app.Persistence.Internal
{

    // Namespaces.
    using Helpers;
    using System.IO;
    using System.Runtime.Caching;


    /// <summary>
    /// A cache of entities that are stored on the file system (helps to avoid
    /// accessing the file system unnecessarily).
    /// </summary>
    internal class EntityFileSystemCache
    {

        #region Properties

        /// <summary>
        /// The cache of entities.
        /// </summary>
        private MemoryCache Entities { get; set; }

        #endregion


        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public EntityFileSystemCache()
        {
            Entities = new MemoryCache("Formulate Entity File System Cache");
        }

        #endregion


        /// <summary>
        /// Returns an entity from the cache.
        /// </summary>
        /// <typeparam name="T">
        /// The type of entity.
        /// </typeparam>
        /// <param name="path">
        /// The path to the entity on the file system.
        /// </param>
        /// <returns>
        /// The entity.
        /// </returns>
        public T Get<T>(string path) where T : class
        {
            var key = path?.ToLower();
            var exists = Entities.Contains(key);
            var item = exists
                ? Entities.Get(key) as T
                : JsonHelper.Deserialize<T>(GetFileContents(path)) as T;
            if (!exists && item != null)
            {
                var policy = new CacheItemPolicy();
                policy.ChangeMonitors.Add(new HostFileChangeMonitor(new[] { path }));
                Entities.Set(key, item, policy);
            }
            return item;
        }


        /// <summary>
        /// Invalidates an item in the cache.
        /// </summary>
        /// <param name="path">
        /// The path to the entity on the file system to invalidate.
        /// </param>
        public void Invalidate(string path)
        {
            var key = path?.ToLower();
            Entities.Remove(key);
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

    }

}