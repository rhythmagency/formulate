namespace formulate.app.Persistence
{

    // Namespaces.
    using Layouts;
    using System;
    using System.Collections.Generic;


    /// <summary>
    /// Interface for persistence of layouts.
    /// </summary>
    public interface ILayoutPersistence
    {
        void Persist(Layout layout);
        void Delete(Guid layoutId);
        void Delete(string layoutAlias);
        Layout Retrieve(Guid layoutId);
        Layout Retrieve(string layoutAlias);
        IEnumerable<Layout> RetrieveChildren(Guid? parentId);
    }

}