namespace formulate.app.Folders
{

    // Namespaces.
    using Entities;
    using System;


    /// <summary>
    /// A folder.
    /// </summary>
    public class Folder : IEntity
    {

        #region Properties

        /// <summary>
        /// The name of this folder.
        /// </summary>
        public string Name { get; set; }


        /// <summary>
        /// The unique identifier of this folder.
        /// </summary>
        public Guid Id { get; set; }


        /// <summary>
        /// The entity path to this folder.
        /// </summary>
        /// <remarks>
        /// This path excludes the root, but includes the folder ID.
        /// </remarks>
        public Guid[] Path { get; set; }

        #endregion

    }

}