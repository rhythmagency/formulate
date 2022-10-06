using System.Runtime.Caching;
using Formulate.Core.Persistence;
using Formulate.Core.Utilities;

namespace Formulate.Web.Persistence
{
    /// <summary>
    /// A cache of entities that are stored on the file system (helps to avoid
    /// accessing the file system unnecessarily).
    /// </summary>
    internal sealed class FileSystemPersistedEntityCache : IPersistedEntityCache
    {
        private readonly IJsonUtility _jsonUtility;

        /// <summary>
        /// The cache of entities.
        /// </summary>
        private readonly MemoryCache _entities;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileSystemPersistedEntityCache"/> class.
        /// </summary>
        /// <param name="jsonUtility"></param>
        public FileSystemPersistedEntityCache(IJsonUtility jsonUtility)
        {
            _jsonUtility = jsonUtility;
            _entities = new MemoryCache("Formulate File System Persisted Entity Cache");
        }

        /// <inheritdoc />
        public TPersistedEntity? Get<TPersistedEntity>(string path) where TPersistedEntity : class, IPersistedEntity
        {
            var key = path?.ToLower();
            var exists = _entities.Contains(key);
            var item = exists
                ? _entities.Get(key) as TPersistedEntity
                : _jsonUtility.Deserialize<TPersistedEntity>(GetFileContents(path));

            if (!exists && item != null)
            {
                var policy = new CacheItemPolicy();
                policy.ChangeMonitors.Add(new HostFileChangeMonitor(new[] { path }));
                _entities.Set(key, item, policy);
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
            _entities.Remove(key);
        }

        /// <summary>
        /// Gets the contents of the file at the specified path.
        /// </summary>
        /// <param name="path">The path to the file.</param>
        /// <returns>
        /// The file contents, or null.
        /// </returns>
        private static string? GetFileContents(string? path)
        {
            if (File.Exists(path))
            {
                return File.ReadAllText(path);
            }

            return default;
        }
    }
}