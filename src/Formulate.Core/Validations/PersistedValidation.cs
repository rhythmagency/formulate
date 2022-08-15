using System;
using System.Runtime.Serialization;
using Formulate.Core.Persistence;

namespace Formulate.Core.Validations
{
    [DataContract]
    public sealed class PersistedValidation : PersistedEntity, IValidationSettings
    {
        /// <summary>
        /// Gets or sets the kind ID.
        /// </summary>
        [DataMember]
        public Guid KindId { get; set; }

        /// <summary>
        /// Gets or sets the configuration.
        /// </summary>
        [DataMember]
        public string Data { get; set; }

        /// <summary>
        /// The alias for this validation.
        /// </summary>
        [DataMember]
        public string Alias { get; set; }
    }
}