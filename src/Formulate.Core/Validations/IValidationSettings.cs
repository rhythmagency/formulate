using Formulate.Core.Types;

namespace Formulate.Core.Validations
{
    /// <summary>
    /// Settings for creating a validation.
    /// </summary>
    public interface IValidationSettings : IEntitySettings
    {
        /// <summary>
        /// The alias for this validation.
        /// </summary>
        public string Alias { get; set; }
    }
}