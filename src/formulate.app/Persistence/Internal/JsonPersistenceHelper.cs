namespace formulate.app.Persistence.Internal
{

    // Namespaces.
    using Entities;
    using Helpers;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;


    /// <summary>
    /// Helps with JSON persistence.
    /// </summary>
    internal class JsonPersistenceHelper
    {

        #region Properties

        /// <summary>
        /// The base folder path to store files in.
        /// </summary>
        private string BasePath { get; set; }


        /// <summary>
        /// The file extension to store files with.
        /// </summary>
        private string Extension { get; set; }


        /// <summary>
        /// The wildcard pattern used to find entity files.
        /// </summary>
        private string WildcardPattern { get; set; }


        /// <summary>
        /// The cache of entities.
        /// </summary>
        private EntityFileSystemCache EntityCache { get; set; }

        #endregion


        #region Constructors

        /// <summary>
        /// Full constructor.
        /// </summary>
        /// <param name="basePath">The base path to store files in.</param>
        /// <param name="extension">The extension to store files with.</param>
        /// <param name="wildcard">
        /// The wildcard pattern that can be used to find entity files.
        /// </param>
        public JsonPersistenceHelper(string basePath, string extension, string wildcard)
        {
            this.BasePath = basePath;
            this.Extension = extension;
            this.WildcardPattern = wildcard;
            this.EntityCache = new EntityFileSystemCache();
        }

        #endregion


        #region Public Methods

        /// <summary>
        /// Writes the specified file at the specified path.
        /// </summary>
        /// <param name="path">The path of the file.</param>
        /// <param name="contents">The contents of the file.</param>
        /// <remarks>
        /// Will ensure the folder exists before attempting to write to the file.
        /// </remarks>
        public void WriteFile(string path, string contents)
        {

            // Ensure folder exists.
            var folderPath = Path.GetDirectoryName(path);
            EnsurePathExists(folderPath);


            // Write file contents.
            File.WriteAllText(path, contents);

        }


        /// <summary>
        /// Gets the file path to the entity with the specified ID.
        /// </summary>
        /// <param name="entityId">The entity's ID.</param>
        /// <returns>The file to the entity's file.</returns>
        public string GetEntityPath(Guid entityId)
        {
            var id = GuidHelper.GetString(entityId);
            var path = Path.Combine(BasePath, id + Extension);
            return path;
        }


        /// <summary>
        /// Ensures that the specified path exists.
        /// </summary>
        /// <param name="path">The path.</param>
        public void EnsurePathExists(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }


        /// <summary>
        /// Persists a entity to the file system.
        /// </summary>
        /// <param name="entityId">The ID of the entity.</param>
        /// <param name="entity">The entity to persist.</param>
        public void Persist(Guid entityId, object entity)
        {
            var path = GetEntityPath(entityId);
            var serialized = JsonHelper.Serialize(entity);
            WriteFile(path, serialized);
            EntityCache.Invalidate(path);
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
                EntityCache.Invalidate(path);
            }
        }


        /// <summary>
        /// Gets the entity with the specified ID.
        /// </summary>
        /// <param name="entityId">The ID of the entity.</param>
        /// <returns>
        /// The entity.
        /// </returns>
        public EntityType Retrieve<EntityType>(Guid entityId) where EntityType : class
        {
            var path = GetEntityPath(entityId);
            return EntityCache.Get<EntityType>(path);
        }


        /// <summary>
        /// Gets all the entities that are the children of the folder with the specified ID.
        /// </summary>
        /// <param name="parentId">The parent ID.</param>
        /// <returns>
        /// The entities.
        /// </returns>
        /// <remarks>
        /// You can specify a parent ID of null to get the root entities.
        /// </remarks>
        public IEnumerable<EntityType> RetrieveChildren<EntityType>(Guid? parentId)
            where EntityType: class, IEntity
        {
            var entities = RetrieveAll<EntityType>();
            if (parentId.HasValue)
            {

                // Return entities under folder.
                return entities.Where(x => x.Path[x.Path.Length - 2] == parentId.Value);

            }
            else
            {

                // Return root entities.
                return entities.Where(x => x.Path.Length == 2);

            }
        }

        #endregion


        #region Private Methods

        /// <summary>
        /// Gets all entities of the specified type.
        /// </summary>
        /// <typeparam name="EntityType">
        /// The type of entity.
        /// </typeparam>
        /// <returns>
        /// The entities.
        /// </returns>
        public IEnumerable<EntityType> RetrieveAll<EntityType>() where EntityType : class
        {
            var entities = new List<EntityType>();
            if (Directory.Exists(BasePath))
            {
                var files = Directory.GetFiles(BasePath, WildcardPattern);
                foreach (var file in files)
                {
                    entities.Add(EntityCache.Get<EntityType>(file));
                }
            }
            return entities;
        }

        #endregion

    }

}