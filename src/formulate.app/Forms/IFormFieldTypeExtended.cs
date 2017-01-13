namespace formulate.app.Forms
{

    /// <summary>
    /// Interface for form field types that require additional, uncommon functionality.
    /// </summary>
    public interface IFormFieldTypeExtended
    {

        /// <summary>
        /// This is set to true on form fields that are transitory (i.e., they aren't
        /// used for data).
        /// </summary>
        /// <remarks>
        /// Some examples include buttons and read only text fields.
        /// </remarks>
        bool IsTransitory { get; }

        /// <summary>
        /// This is set to true on form fields that are server-side only (i.e., they
        /// aren't rendered on the frontend of the website).
        /// </summary>
        bool IsServerSideOnly { get; }

        /// <summary>
        /// This is true for fields that are hidden during data entry.
        /// </summary>
        bool IsHidden { get; }

    }

}