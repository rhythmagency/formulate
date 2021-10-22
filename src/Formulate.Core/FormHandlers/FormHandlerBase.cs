using System;

namespace Formulate.Core.FormHandlers
{
    /// <summary>
    /// The base class for all form handlers.
    /// </summary>
    /// <remarks>Do not implement this type directly. Instead implement <see cref="FormHandler"/> or <see cref="AsyncFormHandler"/>.</remarks>
    public abstract class FormHandlerBase : IFormHandler
    {
        /// <summary>
        /// Gets or sets the type ID.
        /// </summary>
        public Guid TypeId { get; set; }

        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        public Guid Id { get; set; }
        
        /// <summary>
        /// Gets or sets the alias.
        /// </summary>
        public string Alias { get; set; }
        
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Gets or sets a value indicating whether this handler is enabled.
        /// </summary>
        public bool Enabled { get; set; }
    }
}
