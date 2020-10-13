namespace formulate.app.Persistence
{

    // Namespaces.
    using Layouts;
    using System;
    using System.Collections.Generic;


    /// <summary>
    /// Interface for persistence of Layouts.
    /// </summary>
    public interface ILayoutPersistence
    {
        /// <summary>
        /// Persist a Layout.
        /// </summary>
        /// <param name="layout">
        /// The Layout.
        /// </param>
        void Persist(Layout layout);

        /// <summary>
        /// Delete a Layout by ID.
        /// </summary>
        /// <param name="layoutId">
        /// The Layout id.
        /// </param>
        void Delete(Guid layoutId);

        /// <summary>
        /// Delete a Layout by alias.
        /// </summary>
        /// <param name="layoutAlias">
        /// The Layout alias.
        /// </param>
        void Delete(string layoutAlias);

        /// <summary>
        /// Retrieve a Layout by ID.
        /// </summary>
        /// <param name="layoutId">
        /// The Layout id.
        /// </param>
        /// <returns>
        /// A <see cref="Layout"/>.
        /// </returns>
        Layout Retrieve(Guid layoutId);

        /// <summary>
        /// Retrieve a Layout by alias.
        /// </summary>
        /// <param name="layoutAlias">
        /// The Layout alias.
        /// </param>
        /// <returns>
        /// A <see cref="Layout"/>.
        /// </returns>
        Layout Retrieve(string layoutAlias);

        /// <summary>
        /// Retrieve children by their parent ID.
        /// </summary>
        /// <param name="parentId">
        /// The parent id.
        /// </param>
        /// <returns>
        /// If found, a collection of <see cref="Layout"/>.
        /// </returns>
        IEnumerable<Layout> RetrieveChildren(Guid? parentId);
    }
}
