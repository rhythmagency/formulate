using System;

namespace Formulate.Core.FormFields
{
    /// <summary>
    /// The base class for all form fields.
    /// </summary>
    public abstract class FormField : IFormField
    {
        /// <inheritdoc />
        public Guid TypeId { get; }

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
        /// Gets the category.
        /// </summary>

        protected readonly string RawConfiguration;

        /// <summary>
        /// Initializes a new instance of the <see cref="FormField"/> class.
        /// </summary>
        /// <param name="settings">The form field settings.</param>
        /// <exception cref="ArgumentNullException">The settings parameter is null.</exception>
        protected FormField(IFormFieldSettings settings)
        {
            if (settings is null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            TypeId = settings.TypeId;
            Id = settings.Id;
            Alias = settings.Alias;
            Name = settings.Name;
            Label = settings.Label;
            Category = settings.Category;
            RawConfiguration = settings.Configuration;
        }
    }
}