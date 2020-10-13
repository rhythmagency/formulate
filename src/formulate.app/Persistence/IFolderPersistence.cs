namespace formulate.app.Persistence
{

    // Namespaces.
    using Folders;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Interface for persistence of Folders.
    /// </summary>
    public interface IFolderPersistence
    {
        /// <summary>
        /// Persist a Folder.
        /// </summary>
        /// <param name="folder">
        /// The Folder.
        /// </param>
        void Persist(Folder folder);

        /// <summary>
        /// Retrieve a Folder by ID.
        /// </summary>
        /// <param name="folderId">
        /// The Folder id.
        /// </param>
        /// <returns>
        /// The <see cref="Folder"/>.
        /// </returns>
        Folder Retrieve(Guid folderId);

        /// <summary>
        /// Retrieve children by their parent ID.
        /// </summary>
        /// <param name="parentId">
        /// The parent id.
        /// </param>
        /// <returns>
        /// If found, a collection of <see cref="Folder"/>.
        /// </returns>
        IEnumerable<Folder> RetrieveChildren(Guid? parentId);

        /// <summary>
        /// Delete a Folder by ID.
        /// </summary>
        /// <param name="folderId">
        /// The Folder id.
        /// </param>
        void Delete(Guid folderId);
    }
}
