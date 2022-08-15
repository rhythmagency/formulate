namespace Formulate.Core.FormFields
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// A persisted form field.
    /// </summary>
    [DataContract]
    public sealed class PersistedFormField : IFormFieldSettings
    {
        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        [DataMember]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the kind ID.
        /// </summary>
        [DataMember(Name = "TypeId")]
        public Guid KindId { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the configuration.
        /// </summary>
        [DataMember(Name = "FieldConfiguration")]
        public string Data { get; set; }

        /// <summary>
        /// Gets or sets the alias.
        /// </summary>
        [DataMember]
        public string Alias { get; set; }

        /// <summary>
        /// Gets or sets the label.
        /// </summary>
        [DataMember]
        public string Label { get; set; }

        /// <summary>
        /// Gets or sets the category.
        /// </summary>
        [DataMember]
        public string Category { get; set; }

        /// <summary>
        /// Gets or sets the validations.
        /// </summary>
        [DataMember]
        public Guid[] Validations { get; set; } = Array.Empty<Guid>();
    }
}