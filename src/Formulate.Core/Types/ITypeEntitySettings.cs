using System;

namespace Formulate.Core.Types
{
    /// <summary>
    /// A contract for the settings needed to create a <see cref="ITypeEntity"/>.
    /// </summary>
    public interface ITypeEntitySettings
    {
        /// <summary>
        /// Gets the ID.
        /// </summary>
        Guid Id { get; }

        /// <summary>
        /// Gets the type ID.
        /// </summary>
        Guid TypeId { get; }

        /// <summary>
        /// Gets the name.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the configuration.
        /// </summary>
        string Configuration { get; }
    }
}
