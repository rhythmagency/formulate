using System;
using Formulate.Core.Persistence;

namespace Formulate.Core.DataValues
{
    /// <summary>
    /// A persisted data values entity.
    /// </summary>
    public sealed class PersistedDataValues : IPersistedEntity, IDataValuesSettings
    {
        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the definition ID.
        /// </summary>
        public Guid DefinitionId { get; set; }

        /// <summary>
        /// Gets or sets the path.
        /// </summary>
        public Guid[] Path { get; set; }
        
        /// <summary>
        /// Gets or sets name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the configuration.
        /// </summary>
        public string Configuration { get; set; }
    }
}
