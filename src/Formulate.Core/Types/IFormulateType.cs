namespace Formulate.Core.Types
{
    using System;

    using Umbraco.Cms.Core.Composing;

    /// <summary>
    /// The underlying discoverable type for other types identified by a Type ID.
    /// </summary>
    public interface IFormulateType : IDiscoverable
    {
        /// <summary>
        /// Gets the Type ID.
        /// </summary>
        Guid TypeId { get; }
    }
}
