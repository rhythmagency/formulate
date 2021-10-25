using System;

namespace Formulate.Core.FormHandlers
{
    /// <summary>
    /// The base class for all form handler definitions.
    /// </summary>
    /// <remarks>Do not implement this definition directly. Instead implement <see cref="FormHandlerDefinition"/> or <see cref="AsyncFormHandlerDefinition"/>.</remarks>
    public abstract class FormHandlerDefinitionBase : IFormHandlerDefinition
    {
        /// <inheritdoc />
        public abstract Guid DefinitionId { get; }
    }
}
