using Formulate.Core.Persistence;
using Formulate.Core.Utilities;
using Microsoft.Extensions.Logging;

namespace Formulate.Web.Persistence
{
    /// <summary>
    /// A utility to help with repository for a given type.
    /// </summary>
    internal sealed class WebHostRepositoryUtility<TPersistedEntity> : IRepositoryUtility<TPersistedEntity> where TPersistedEntity : class, IPersistedEntity
    {
        /// <summary>
        /// The base folder path to store files in.
        /// </summary>
        private readonly string _basePath;

        /// <summary>
        /// The file extension to store files with.
        /// </summary>
        private readonly string _extension;

        /// <summary>
        /// The wildcard pattern used to find entity files.
        /// </summary>
        private readonly string _wildcardPattern;

        /// <summary>
        /// The json utility.
        /// </summary>
        private readonly IJsonUtility _jsonUtility;

        /// <summary>
        /// The cache of entities.
        /// </summary>
        private readonly IPersistedEntityCache _entityCache;

        /// <summary>
        /// The logger.
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        /// Full constructor.
        /// </summary>
        /// <param name="settings">The repository helper settings.</param>
        /// <param name="jsonUtility">The json utility.</param>
        /// <param name="entityCache">The entity cache.</param>
        /// <param name="logger"></param>
        public WebHostRepositoryUtility(WebHostRepositoryUtilitySettings settings, IJsonUtility jsonUtility,
            IPersistedEntityCache entityCache, ILogger logger)
        {
            _basePath = settings.BasePath;
            _extension = settings.Extension;
            _wildcardPattern = settings.Wildcard;
            _jsonUtility = jsonUtility;
            _entityCache = entityCache;
            _logger = logger;
        }

        /// <summary>
        /// Persists a entity to the file system.
        /// </summary>
        /// <param name="entity">The entity to persist.</param>
        public TPersistedEntity Save(TPersistedEntity entity)
        {
            var path = GetEntityPath(entity.Id);
            var serialized = _jsonUtility.Serialize(entity);
            WriteFile(path, serialized);
            _entityCache.Invalidate(path);

            return entity;
        }

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entityId">The ID of the entity to delete.</param>
        public void Delete(Guid entityId)
        {
            var path = GetEntityPath(entityId);
            if (File.Exists(path))
            {
                File.Delete(path);
                _entityCache.Invalidate(path);
            }
        }

        /// <summary>
        /// Gets the entity with the specified ID.
        /// </summary>
        /// <param name="entityId">The ID of the entity.</param>
        /// <returns>
        /// A <typeparamref name="TPersistedEntity"/>.
        /// </returns>
        public TPersistedEntity Get(Guid entityId)
        {
            var path = GetEntityPath(entityId);

            return _entityCache.Get<TPersistedEntity>(path);
        }

        /// <summary>
        /// Gets any root level items.
        /// </summary>
        /// <returns>
        /// A read only collection of <typeparamref name="TPersistedEntity"/>.
        /// </returns>
        public IReadOnlyCollection<TPersistedEntity> GetRootItems()
        {
            var entities = RetrieveAll();

            return entities.Where(x => x.Path.Length == 2).ToArray();
        }

        public Guid[] Move(TPersistedEntity entity, Guid[] newPath)
        {
            Save(entity);

            return entity.Path;
        }

        /// <summary>
        /// Gets all the entities that are the children of the folder with the specified ID.
        /// </summary>
        /// <param name="parentId">The parent ID.</param>
        /// <returns>
        /// The entities.
        /// </returns>
        public IReadOnlyCollection<TPersistedEntity> GetChildren(Guid parentId)
        {
            var entities = RetrieveAll();

            if (entities is null)
            {
                return Array.Empty<TPersistedEntity>();
            }

            // Return entities under folder.
            return entities.Where(x => x.ParentId() == parentId).ToArray();
        }

        /// <summary>
        /// Gets the file path to the entity with the specified ID.
        /// </summary>
        /// <param name="entityId">The entity's ID.</param>
        /// <returns>The file to the entity's file.</returns>
        private string GetEntityPath(Guid entityId)
        {
            var id = entityId.ToString("N");
            var path = Path.Combine(_basePath, id + _extension);
            return path;
        }

        /// <summary>
        /// Writes the specified file at the specified path.
        /// </summary>
        /// <param name="path">The path of the file.</param>
        /// <param name="contents">The contents of the file.</param>
        /// <remarks>
        /// Will ensure the folder exists before attempting to write to the file.
        /// </remarks>
        private static void WriteFile(string path, string contents)
        {
            // Ensure folder exists.
            var folderPath = Path.GetDirectoryName(path);
            EnsurePathExists(folderPath);

            // Write file contents.
            File.WriteAllText(path, contents);
        }

        /// <summary>
        /// Ensures that the specified path exists.
        /// </summary>
        /// <param name="path">The path.</param>
        private static void EnsurePathExists(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        /// <summary>
        /// Gets all entities of the specified type.
        /// </summary>
        /// <returns>
        /// A read only collection of <typeparamref name="TPersistedEntity"/>.
        /// </returns>
        private IReadOnlyCollection<TPersistedEntity> RetrieveAll()
        {
            var entities = new List<TPersistedEntity>();

            if (Directory.Exists(_basePath) == false)
            {
                return entities;
            }

            var files = Directory.GetFiles(_basePath, _wildcardPattern);
            
            foreach (var file in files)
            {
                var entity = _entityCache.Get<TPersistedEntity>(file);

                if (entity is not null)
                {
                    entities.Add(entity);
                }
            }

            return entities;
        }
    }
}
