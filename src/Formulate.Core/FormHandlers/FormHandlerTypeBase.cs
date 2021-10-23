using System;

namespace Formulate.Core.FormHandlers
{
    /// <summary>
    /// The base class for all form handler types.
    /// </summary>
    /// <remarks>Do not implement this type directly. Instead implement <see cref="FormHandlerType"/> or <see cref="AsyncFormHandlerType"/>.</remarks>
    public abstract class FormHandlerTypeBase : IFormHandlerType
    {
        /// <summary>
        /// Gets the type ID.
        /// </summary>
        public abstract Guid TypeId { get; }
    }
}
