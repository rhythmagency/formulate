using System;
using Formulate.Core.Persistence;

namespace Formulate.Core.Validations
{
    public sealed class PersistedValidation : PersistedEntity, IValidationSettings
    {
        /// <summary>
        /// Gets or sets the definition ID.
        /// </summary>
        public Guid DefinitionId { get; set; }
        
        /// <summary>
        /// Gets or sets the configuration.
        /// </summary>
        public string Configuration { get; set; }
    }
}
