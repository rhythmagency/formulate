namespace Formulate.BackOffice.Controllers.Forms
{
    // Namespaces.
    using Core.Forms;
    using System;

    /// <summary>
    /// The request to save a form.
    /// </summary>
    public sealed class SavePersistedFormRequest
    {
        /// <summary>
        /// The form data to save.
        /// </summary>
        public PersistedForm Entity { get; set; }

        /// <summary>
        /// The ID of the entity (e.g., a folder) the form resides within.
        /// </summary>
        public Guid? ParentId { get; set; }
    }
}