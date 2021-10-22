namespace Formulate.Core.Types
{
    using System;

    /// <summary>
    /// The underlying type for other types identified by a Type ID.
    /// </summary>
    public interface IFormulateType
    {
        /// <summary>
        /// Gets the Type ID.
        /// </summary>
        Guid TypeId { get; }
    }
}
