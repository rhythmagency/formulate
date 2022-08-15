using System;
using System.Runtime.Serialization;
using Formulate.Core.FormFields;
using Formulate.Core.FormHandlers;
using Formulate.Core.Persistence;

namespace Formulate.Core.Forms
{
    /// <summary>
    /// A persisted form entity.
    /// </summary>
    [DataContract]
    public sealed class PersistedForm : PersistedEntity
    {
        /// <summary>
        /// Gets or sets the fields.
        /// </summary>
        [DataMember]
        public PersistedFormField[] Fields { get; set; } = Array.Empty<PersistedFormField>();

        /// <summary>
        /// Gets or sets the handlers.
        /// </summary>
        [DataMember]
        public PersistedFormHandler[] Handlers { get; set; } = Array.Empty<PersistedFormHandler>();
    }
}
