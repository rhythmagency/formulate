using System;

namespace Formulate.Core.Types
{
    /// <summary>
    /// The underlying definition for entities created by a <see cref="IDefinition"/>.
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// Gets the ID.
        /// </summary>
        Guid Id { get; }

        /// <summary>
        /// Gets the kind ID.
        /// </summary>
        Guid KindId { get; }
    }
}
