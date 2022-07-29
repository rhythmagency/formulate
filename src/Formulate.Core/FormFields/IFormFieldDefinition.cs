namespace Formulate.Core.FormFields
{
    // Namespaces.
    using Types;

    /// <summary>
    /// A contract for implementing a form field definition.
    /// </summary>
    public interface IFormFieldDefinition : IDefinition, IFormFieldFeatures
    {
        /// <summary>
        /// Gets the icon.
        /// </summary>
        string Icon { get; }

        /// <summary>
        /// Gets the group.
        /// </summary>
        /// <remarks>This is used to categorize this form field with similar fields (e.g. Input, Content).</remarks>
        string Category { get; }

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