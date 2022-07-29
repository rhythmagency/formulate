namespace Formulate.BackOffice.ViewModels.Forms
{
    // Namespaces.
    using System;

    /// <summary>
    /// The data needed by the back office for a form field validation.
    /// </summary>
    public sealed class FormFieldValidationEditorModel
    {
        /// <summary>
        /// The configuration for this validation.
        /// </summary>
        public object Configuration { get; set; }

        /// <summary>
        /// The ID of this validation.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The name of this validation.
        /// </summary>
        public string Name { get; set; }
    }
}