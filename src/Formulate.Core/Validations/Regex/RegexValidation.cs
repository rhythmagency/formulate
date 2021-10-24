namespace Formulate.Core.Validations.Regex
{
    /// <summary>
    /// A validation kind that validates against a regular expression.
    /// </summary>
    public sealed class RegexValidation : Validation<RegexValidationConfiguration>
    {
        /// <inheritdoc />
        public RegexValidation(IValidationSettings settings, RegexValidationConfiguration configuration) : base(settings, configuration)
        {
        }
    }
}
