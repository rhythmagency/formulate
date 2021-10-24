namespace Formulate.Core.Validations.Mandatory
{
    /// <summary>
    /// A mandatory validation.
    /// </summary>
    public sealed class MandatoryValidation : Validation<MandatoryValidationConfiguration>
    {
        /// <inheritdoc />
        public MandatoryValidation(IValidationSettings settings, MandatoryValidationConfiguration configuration) : base(settings, configuration)
        {
        }
    }
}
