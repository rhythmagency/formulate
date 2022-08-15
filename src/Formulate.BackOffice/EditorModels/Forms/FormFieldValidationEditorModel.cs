namespace Formulate.BackOffice.EditorModels.Forms
{
    // Namespaces.
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// The data needed by the back office for a form field validation.
    /// </summary>
    [DataContract]
    public sealed class FormFieldValidationEditorModel
    {
        /// <summary>
        /// The ID of this validation.
        /// </summary>
        [DataMember(Name = "id")]
        public Guid Id { get; set; }

        /// <summary>
        /// The name of this validation.
        /// </summary>
        [DataMember(Name = "name")]
        public string Name { get; set; }
    }
}