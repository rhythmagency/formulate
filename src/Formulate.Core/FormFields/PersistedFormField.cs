namespace Formulate.Core.FormFields
{
    // Namespaces.
    using System;
    using System.Text.Json.Serialization;

    /// <summary>
    /// A persisted form field.
    /// </summary>
    public sealed class PersistedFormField : IFormFieldSettings
    {
        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the kind ID.
        /// </summary>
        [JsonPropertyName("TypeId")]
        public Guid KindId { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the configuration.
        /// </summary>
        public string Data { get; set; }

        /// <summary>
        /// Gets or sets the alias.
        /// </summary>
        public string Alias { get; set; }

        /// <summary>
        /// Gets or sets the label.
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// Gets or sets the category.
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// Gets or sets the validations.
        /// </summary>
        public Guid[] Validations { get; set; }
    }
}