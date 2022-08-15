namespace Formulate.Core.Validations.Regex
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Configuration used by <see cref="RegexValidation"/>.
    /// </summary>
    [DataContract]
    public sealed class RegexValidationConfiguration
    {
        /// <summary>
        /// Gets or sets the regular expression pattern.
        /// </summary>
        [DataMember(Name = "regex")]
        public string Regex { get; set; }

        /// <summary>
        /// Gets or sets the error message to show when the validation fails.
        /// </summary>
        [DataMember(Name = "message")]
        public string Message { get; set; }
    }
}
