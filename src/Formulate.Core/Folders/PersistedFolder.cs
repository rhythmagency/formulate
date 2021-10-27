using System;
using Formulate.Core.Persistence;

namespace Formulate.Core.Folders
{
    /// <summary>
    /// A persisted folder entity.
    /// </summary>
    public sealed class PersistedFolder : IPersistedEntity
    {
        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the path.
        /// </summary>
        public Guid[] Path { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }
    }
}
