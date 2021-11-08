using System;
using Formulate.Core.Persistence;

namespace Formulate.Core.DataValues
{
    /// <summary>
    /// A persisted data values entity.
    /// </summary>
    public sealed class PersistedDataValues : PersistedEntity, IDataValuesSettings
    {
        /// <summary>
        /// Gets or sets the definition ID.
        /// </summary>
        public Guid DefinitionId { get; set; }

        /// <summary>
        /// Gets or sets the configuration.
        /// </summary>
        public string Configuration { get; set; }
    }
}
