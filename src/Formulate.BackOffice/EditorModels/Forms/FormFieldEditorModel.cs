namespace Formulate.BackOffice.EditorModels.Forms
{
    // Namespaces.
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Represents a form field that will be passed to the back office.
    /// </summary>
    [DataContract]
    public sealed class FormFieldEditorModel : ItemEditorModel
    {
        /// <summary>
        /// The field configuration.
        /// </summary>
        [DataMember(Name = "configuration")]
        public object Configuration { get; set; }

        /// <summary>
        /// The field category.
        /// </summary>
        [DataMember(Name = "category")]
        public string Category { get; set; }

        /// <summary>
        /// The icon corresponding to the field type.
        /// </summary>
        [DataMember(Name = "icon")]
        public string Icon { get; set; }

        /// <summary>
        /// The ID corresponding to the type of field this is.
        /// </summary>
        [DataMember(Name = "kindId")]
        public Guid KindId { get; set; }

        /// <summary>
        /// The label for this field.
        /// </summary>
        [DataMember(Name = "label")]
        public string Label { get; set; }

        /// <summary>
        /// The validations applied to this field.
        /// </summary>
        [DataMember(Name = "validations")]
        public FormFieldValidationEditorModel[] Validations { get; set; } = Array.Empty<FormFieldValidationEditorModel>();

        /// <summary>
        /// The directive for this field.
        /// </summary>
        [DataMember(Name = "directive")]
        public string Directive { get; set; }

        /// <summary>
        /// Whether this field supports validation.
        /// </summary>
        [DataMember(Name = "supportsValidation")]
        public bool SupportsValidation { get; set; }
    }
}