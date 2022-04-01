namespace Formulate.BackOffice.Controllers.Forms
{
    // Namespaces.
    using Core.Forms;

    /// <summary>
    /// The request to save a form.
    /// </summary>
    public sealed class SavePersistedFormRequest
    {
        /// <summary>
        /// The form data to save.
        /// </summary>
        public PersistedForm Entity { get; set; }
    }
}