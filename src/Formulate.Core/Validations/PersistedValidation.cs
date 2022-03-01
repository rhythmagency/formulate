using System;
using Formulate.Core.Persistence;

namespace Formulate.Core.Validations
{
    public sealed class PersistedValidation : PersistedEntity, IValidationSettings
    {
        /// <summary>
        /// Gets or sets the kind ID.
        /// </summary>
        public Guid KindId { get; set; }
        
        /// <summary>
        /// Gets or sets the configuration.
        /// </summary>
        public string Data { get; set; }
    }
}
