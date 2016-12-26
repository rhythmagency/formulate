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

    }

}