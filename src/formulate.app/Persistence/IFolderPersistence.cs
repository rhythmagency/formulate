namespace formulate.app.Persistence
{

    // Namespaces.
    using Folders;
    using System;
    using System.Collections.Generic;


    /// <summary>
    /// Interface for persistence of folders.
    /// </summary>
    public interface IFolderPersistence
    {
        void Persist(Folder folder);
        Folder Retrieve(Guid folderId);
        IEnumerable<Folder> RetrieveChildren(Guid? parentId);
    }

}