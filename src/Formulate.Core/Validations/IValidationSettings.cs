using System;
using Formulate.Core.Types;

namespace Formulate.Core.Validations
{
    /// <summary>
    /// Settings for creating a validation.
    /// </summary>
    public interface IValidationSettings : IFormulateType
    {
        /// <summary>
        /// Gets the ID.
        /// </summary>
        Guid Id { get; }

        /// <summary>
        /// Gets the configuration.
        /// </summary>
        string Configuration { get; }
    }
}
