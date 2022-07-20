namespace Formulate.Core.FormFields
{
    // Namespaces.
    using Types;

    /// <summary>
    /// A contract for implementing a form field definition.
    /// </summary>
    public interface IFormFieldDefinition : IDefinition
    {
        /// <summary>
        /// Gets the icon.
        /// </summary>
        string Icon { get; }
        
        /// <summary>
        /// Gets a value indicating whether this field definition is transitory.
        /// </summary>
        bool IsTransitory { get; }

        /// <summary>
        /// Gets a value indicating whether this field definition is server side only.
        /// </summary>
        bool IsServerSideOnly { get; }

        /// <summary>
        /// Gets a value indicating whether this field definition is hidden.
        /// </summary>
        bool IsHidden { get; }

        /// <summary>
        /// Gets a value indicating whether this field definition is stored.
        /// </summary>
        bool IsStored { get; }

        /// <summary>
        /// Creates a new instance of a <see cref="FormField"/>.
        /// </summary>
        /// <param name="settings">
        /// The current form field settings.
        /// </param>
        /// <returns>
        /// A <see cref="FormField"/>.
        /// </returns>
        FormField CreateField(IFormFieldSettings settings);

        /// <summary>
        /// Creates an instance of the configuration needed by the back
        /// office.
        /// </summary>
        /// <param name="settings">
        /// The current form field settings.
        /// </param>
        /// <returns>
        /// The configuration.
        /// </returns>
        object GetBackOfficeConfiguration(IFormFieldSettings settings);
    }
}