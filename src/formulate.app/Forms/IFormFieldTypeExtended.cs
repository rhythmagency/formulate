namespace formulate.app.Forms
{

    // Namespaces.
    using System.Collections.Generic;


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


        /// <summary>
        /// Is the field value valid?
        /// </summary>
        /// <param name="value">The value submitted with the form.</param>
        /// <returns>
        /// True, if the value is valid; otherwise, false.
        /// </returns>
        bool IsValid(IEnumerable<string> value);


        /// <summary>
        /// Is this field value stored (e.g., reCAPTCHA values would not be stored)?
        /// </summary>
        bool IsStored { get; }

    }

}