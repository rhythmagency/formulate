using System;
using Formulate.Core.Types;

namespace Formulate.Core.FormHandlers
{
    /// <summary>
    /// Settings for creating a form handler.
    /// </summary>
    public interface IFormHandlerSettings : ITypeEntitySettings
    {
        /// <summary>
        /// Gets the alias.
        /// </summary>
        string Alias { get; }

        /// <summary>
        /// Gets a value indicating whether this is enabled.
        /// </summary>
        bool Enabled { get; }
    }
}
