using System;

namespace Formulate.Core.FormHandlers
{
    /// <summary>
    /// A base contract for creating a Form Handler.
    /// </summary>
    /// <remarks>Do not implement this type directly. Instead implement <see cref="FormHandler"/> or <see cref="AsyncFormHandler"/>.</remarks>
    public interface IFormHandler
    {
        /// <summary>
        /// Gets or sets the type id.
        /// </summary>
        Guid TypeId { get; set; }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the alias.
        /// </summary>
        string Alias { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this is enabled.
        /// </summary>
        bool Enabled { get; set; }
    }
}
