namespace Formulate.Core.Types
{
    using System;
    
    /// <summary>
    /// The underlying type for other types identified by a Type ID.
    /// </summary>
    public interface IType
    {
        /// <summary>
        /// Gets the type ID.
        /// </summary>
        Guid TypeId { get; }
    }
}
