namespace formulate.app.Forms
{

    // Namespaces.
    using System.Collections.Generic;

    /// <summary>
    /// A contract for creating a Form field that requires additional, uncommon functionality.
    /// </summary>
    public interface IFormFieldTypeExtended
    {
        /// <summary>
        /// Gets a value indicating whether this is transitory (i.e., they aren't used for data) 
        /// </summary>
        /// <remarks>
        /// Some examples include buttons and read only text fields.
        /// </remarks>
        bool IsTransitory { get; }

        /// <summary>
        /// Gets a value indicating whether this is server-side only (i.e., they aren't rendered on the frontend of the website).
        /// </summary>
        bool IsServerSideOnly { get; }

        /// <summary>
        /// Gets a value indicating whether this is hidden during data entry.
        /// </summary>
        bool IsHidden { get; }

        /// <summary>
        /// Gets a value indicating whether this is field value is stored (e.g., reCAPTCHA values would not be stored).
        /// </summary>
        bool IsStored { get; }

        /// <summary>
        /// Is the field value valid?
        /// </summary>
        /// <param name="value">The value submitted with the form.</param>
        /// <returns>
        /// True, if the value is valid; otherwise, false.
        /// </returns>
        bool IsValid(IEnumerable<string> value);

        /// <summary>
        /// Returns a validation message that is shown for fields that have validation
        /// built in (i.e., rather than relying on the messages specific to validations
        /// that can be attached to any field).
        /// </summary>
        /// <returns>
        /// The validation error message.
        /// </returns>
        string GetNativeFieldValidationMessage();
    }
}
