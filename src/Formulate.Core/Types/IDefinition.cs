using Umbraco.Cms.Core.Composing;

namespace Formulate.Core.Types
{
    using System;
    
    /// <summary>
    /// The underlying definition for other definitions identified by a kind ID.
    /// </summary>
    public interface IDefinition : IDiscoverable
    {
        /// <summary>
        /// Gets the kind ID.
        /// </summary>
        Guid KindId { get; }

        /// <summary>
        /// Gets the name.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the directive.
        /// </summary>
        string Directive { get; }

        /// <summary>
        /// Gets if this is a legacy definition.
        /// </summary>
        /// <remarks>Legacy definitions can not have new entities created from them and exist for older data.</remarks>
        bool IsLegacy { get; }
    }
}
