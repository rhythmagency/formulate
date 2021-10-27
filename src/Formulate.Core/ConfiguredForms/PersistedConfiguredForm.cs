using System;
using Formulate.Core.Persistence;

namespace Formulate.Core.ConfiguredForms
{
    /// <summary>
    /// A persisted configured form entity.
    /// </summary>
    public sealed class PersistedConfiguredForm : IPersistedEntity, IConfiguredFormSettings
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
