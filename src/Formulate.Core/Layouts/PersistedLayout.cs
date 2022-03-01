using System;
using Formulate.Core.Persistence;

namespace Formulate.Core.Layouts
{
    /// <summary>
    /// A persisted layout entity.
    /// </summary>
    public sealed class PersistedLayout : PersistedEntity, ILayoutSettings
    {
        /// <summary>
        /// Gets or sets the kind ID.
        /// </summary>
        public Guid KindId { get; set; }
        
        /// <summary>
        /// Gets or sets the configuration.
        /// </summary>
        public string Configuration { get; set; }
    }
}
