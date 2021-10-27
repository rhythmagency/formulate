using System;
using Formulate.Core.FormFields;
using Formulate.Core.FormHandlers;
using Formulate.Core.Persistence;

namespace Formulate.Core.Forms
{
    /// <summary>
    /// A persisted form entity.
    /// </summary>
    public sealed class PersistedForm : IPersistedEntity
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

        /// <summary>
        /// Gets or sets the alias.
        /// </summary>
        public string Alias { get; set; }
        
        /// <summary>
        /// Gets or sets the fields.
        /// </summary>
        public PersistedFormField[] Fields { get; set; }
        
        /// <summary>
        /// Gets or sets the handlers.
        /// </summary>
        public PersistedFormHandler[] Handlers { get; set; }
    }
}
