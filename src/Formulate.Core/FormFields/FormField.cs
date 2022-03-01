using System;
using System.Collections.Generic;
using Formulate.Core.Validations;

namespace Formulate.Core.FormFields
{
    /// <summary>
    /// The extended base class for form fields with a configuration.
    /// </summary>
    /// <definitionparam name="TConfig">The definition of the form field configuration.</definitionparam>
    public abstract class FormField<TConfig> : FormField
    {
        /// <summary>
        /// Gets the configuration.
        /// </summary>
        public TConfig Configuration { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="FormField"/> class.
        /// </summary>
        /// <param name="settings">The form field settings.</param>
        /// <param name="configuration">The form field configuration.</param>
        /// <exception cref="ArgumentNullException">The settings parameter is null.</exception>
        protected FormField(IFormFieldSettings settings, TConfig configuration) : base(settings)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FormField"/> class.
        /// </summary>
        /// <param name="settings">The form field settings.</param>
        /// <param name="validations">The validations.</param>
        /// <param name="configuration">The form field configuration.</param>
        /// <definitionparam name="TConfig">The definition of the form field configuration.</definitionparam>
        /// <exception cref="ArgumentNullException">The settings parameter is null.</exception>
        protected FormField(IFormFieldSettings settings, IReadOnlyCollection<IValidation> validations, TConfig configuration) : base(settings, validations)
        {
            Configuration = configuration;
        }
    }

    /// <summary>
    /// The base class for all form fields.
    /// </summary>
    public abstract class FormField : IFormField
    {
        /// <inheritdoc />
        public Guid KindId { get; }

        /// <summary>
        /// Gets the id.
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// Gets the alias.
        /// </summary>
        public string Alias { get; }

        /// <summary>
        /// Gets the name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the label.
        /// </summary>
        public string Label { get; }

        /// <summary>
        /// Gets the category.
        /// </summary>
        public string Category { get; }

        /// <summary>
        /// Gets the validations.
        /// </summary>
        public IReadOnlyCollection<IValidation> Validations { get; }

        /// <summary>
        /// The raw configuration.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This is for reference only.
        /// </para>
        /// <para>
        /// Deserialization should typically happen in the overridden <see cref="IFormFieldDefinition"/> CreateField method.
        /// </para>
        /// </remarks>
        protected readonly string RawConfiguration;

        /// <summary>
        /// Initializes a new instance of the <see cref="FormField"/> class.
        /// </summary>
        /// <param name="settings">The form field settings.</param>
        /// <exception cref="ArgumentNullException">The settings parameter is null.</exception>
        protected FormField(IFormFieldSettings settings) : this(settings, default)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FormField"/> class.
        /// </summary>
        /// <param name="settings">The form field settings.</param>
        /// <param name="validations">The validations.</param>
        /// <exception cref="ArgumentNullException">The settings parameter is null.</exception>
        protected FormField(IFormFieldSettings settings, IReadOnlyCollection<IValidation> validations)
        {
            if (settings is null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            KindId = settings.KindId;
            Id = settings.Id;
            Alias = settings.Alias;
            Name = settings.Name;
            Label = settings.Label;
            Category = settings.Category;
            Validations = validations ?? Array.Empty<IValidation>();
            RawConfiguration = settings.Data;
        }
    }
}