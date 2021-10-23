using System;
using Formulate.Core.Types;

namespace Formulate.Core.FormHandlers
{
    /// <summary>
    /// Settings for creating a form handler.
    /// </summary>
    public interface IFormHandlerSettings : IFormulateType
    {
        /// <summary>
        /// Gets the ID.
        /// </summary>
        Guid Id { get; }
        
        /// <summary>
        /// Gets the name.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the alias.
        /// </summary>
        string Alias { get; }

        /// <summary>
        /// Gets a value indicating whether this is enabled.
        /// </summary>
        bool Enabled { get; }

        /// <summary>
        /// Gets the configuration.
        /// </summary>
        string Configuration { get; }
    }
}
