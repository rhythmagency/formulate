namespace Formulate.BackOffice.ViewModels.Forms
{
    // Namespaces.
    using System;

    /// <summary>
    /// Represents a form field that will be passed to the back office.
    /// </summary>
    internal class FieldViewModel
    {
        /// <summary>
        /// The field alias.
        /// </summary>
        public string Alias { get; set; }

        /// <summary>
        /// The field name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The field configuration.
        /// </summary>
        public object Configuration { get; set; }

        /// <summary>
        /// The field category.
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// The icon corresponding to the field type.
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// The ID of this field.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The ID corresponding to the type of field this is.
        /// </summary>
        public Guid KindId { get; set; }

        /// <summary>
        /// The label for this field.
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// The validations applied to this field.
        /// </summary>
        public ValidationViewModel[] Validations { get; set; }
    }
}