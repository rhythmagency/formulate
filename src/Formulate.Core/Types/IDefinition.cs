using Umbraco.Cms.Core.Composing;

namespace Formulate.Core.Types
{
    using System;
    
    /// <summary>
    /// The underlying definition for other definitions identified by a definition ID.
    /// </summary>
    public interface IDefinition : IDiscoverable
    {
        /// <summary>
        /// Gets the definition ID.
        /// </summary>
        Guid DefinitionId { get; }

        /// <summary>
        /// Gets the definition label.
        /// </summary>
        string DefinitionLabel { get; }

        /// <summary>
        /// Gets the directive.
        /// </summary>
        string Directive { get; }
    }
}
