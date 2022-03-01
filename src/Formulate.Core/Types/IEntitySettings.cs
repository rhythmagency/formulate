using System;

namespace Formulate.Core.Types
{
    /// <summary>
    /// A contract for the settings needed to create a <see cref="IEntity"/>.
    /// </summary>
    public interface IEntitySettings
    {
        /// <summary>
        /// Gets the ID.
        /// </summary>
        Guid Id { get; }

        /// <summary>
        /// Gets the kind ID.
        /// </summary>
        Guid KindId { get; }

        /// <summary>
        /// Gets the name.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the configuration.
        /// </summary>
        string Data { get; }
    }
}
