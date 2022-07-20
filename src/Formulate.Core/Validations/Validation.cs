using System;

namespace Formulate.Core.Validations
{
    /// <summary>
    /// The extended base class for validations with a configuration.
    /// </summary>
    /// <definitionparam name="TConfig">The definition of the validation configuration.</definitionparam>
    public abstract class Validation<TConfig> : Validation
    {
        /// <summary>
        /// Gets the configuration.
        /// </summary>
        public TConfig Configuration { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Validation"/> class.
        /// </summary>
        /// <param name="settings">The validation settings.</param>
        /// <param name="configuration">The validation configuration.</param>
        /// <exception cref="ArgumentNullException">The settings parameter is null.</exception>
        protected Validation(IValidationSettings settings, TConfig configuration) : base(settings)
        {
            Configuration = configuration;
        }
    }

    /// <summary>
    /// The base class for all validations.
    /// </summary>
    public abstract class Validation : IValidation
    {
        /// <inheritdoc />
        public Guid Id { get; }

        /// <inheritdoc />
        public Guid KindId { get; }

        /// <inheritdoc />
        public string Name { get; set; }

        /// <summary>
        /// The raw configuration.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This is for reference only.
        /// </para>
        /// <para>
        /// Deserialization should typically happen in the overridden <see cref="IValidationDefinition"/> CreateValidation method.
        /// </para>
        /// </remarks>
        public string RawConfiguration { get; set; }

        /// <summary>
        /// The configuration used by the back office for this validation.
        /// </summary>
        public object BackOfficeConfiguration { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Validation"/> class.
        /// </summary>
        /// <param name="settings">The validation settings.</param>
        /// <exception cref="ArgumentNullException">The settings parameter is null.</exception>
        protected Validation(IValidationSettings settings)
        {
            if (settings is null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            Id = settings.Id;
            KindId = settings.KindId;
            RawConfiguration = settings.Data;
        }
    }
}