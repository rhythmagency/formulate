using System;

namespace Formulate.Core.Types
{
    /// <summary>
    /// The underlying type for entities created by a <see cref="IType"/>.
    /// </summary>
    public interface ITypeEntity
    {
        /// <summary>
        /// Gets the ID.
        /// </summary>
        Guid Id { get; }


        /// <summary>
        /// Gets the type ID.
        /// </summary>
        Guid TypeId { get; }
    }
}
