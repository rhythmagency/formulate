namespace Formulate.Core.Validations.Mandatory
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Configuration used by <see cref="MandatoryValidation"/>.
    /// </summary>
    [DataContract]
    public sealed class MandatoryValidationConfiguration
    {
        /// <summary>
        /// Gets or sets the error message to show when the validation fails.
        /// </summary>
        [DataMember(Name = "message")]
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to only validate on the client side (i.e., skip server-side validation)?.
        /// </summary>
        [DataMember(Name = "clientSideOnly")]
        public bool ClientSideOnly { get; set; }
    }
}
