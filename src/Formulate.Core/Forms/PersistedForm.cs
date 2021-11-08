using System;
using Formulate.Core.FormFields;
using Formulate.Core.FormHandlers;
using Formulate.Core.Persistence;

namespace Formulate.Core.Forms
{
    /// <summary>
    /// A persisted form entity.
    /// </summary>
    public sealed class PersistedForm : PersistedEntity
    {
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
