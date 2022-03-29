namespace Formulate.Core.FormFields
{
    // Namespaces.
    using System;
    using System.Collections.Generic;
    using Validations;

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

        /// <inheritdoc />
        public Guid Id { get; }

        /// <inheritdoc />
        public string Alias { get; }

        /// <inheritdoc />
        public string Name { get; }

        /// <inheritdoc />
        public string Label { get; }

        /// <inheritdoc />
        public string Category { get; }

        /// <inheritdoc />
        public IReadOnlyCollection<IValidation> Validations { get; }

        /// <summary>
        /// Gets or sets the icon for this form field.
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// Gets or sets the AngularJS directive for this form field.
        /// </summary>
        public string Directive { get; set; }

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