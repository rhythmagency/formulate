namespace Formulate.Core.Types
{
    using System;
    
    /// <summary>
    /// The underlying definition for other definitions identified by a definition ID.
    /// </summary>
    public interface IDefinition
    {
        /// <summary>
        /// Gets the definition ID.
        /// </summary>
        Guid DefinitionId { get; }
    }
}
