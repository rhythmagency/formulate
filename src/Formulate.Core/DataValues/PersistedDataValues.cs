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
        /// Gets or sets the kind ID.
        /// </summary>
        public Guid KindId { get; set; }

        /// <summary>
        /// Gets or sets the configuration.
        /// </summary>
        public string Data { get; set; }
    }
}
