namespace Formulate.Core.FormHandlers
{
    // Namespaces.
    using Formulate.Core.Types;

    /// <summary>
    /// A contract for implementing form handler definition.
    /// </summary>
    /// <remarks>
    /// Do not implement this definition directly.
    /// Instead implement <see cref="FormHandlerDefinition"/>.
    /// </remarks>
    public interface IFormHandlerDefinition : IDefinition
    {
        /// <summary>
        /// Gets the icon for this form handle definition.
        /// </summary>
        string Icon { get; }

        /// <summary>
        /// Gets the category.
        /// </summary>
        /// <remarks>This is used to categorize this form handlers with similar handlers (e.g. General, SalesForce, HubSpot).</remarks>
        string Category { get; }

        /// <summary>
        /// Creates a Form Handler.
        /// </summary>
        /// <param name="settings">The form handler settings.</param>
        /// <returns>A <see cref="FormHandler"/>.</returns>
        FormHandler CreateHandler(IFormHandlerSettings settings);

        /// <summary>
        /// Creates an instance of the configuration needed by the back
        /// office.
        /// </summary>
        /// <param name="settings">
        /// The current form handler settings.
        /// </param>
        /// <returns>
        /// The configuration.
        /// </returns>
        object GetBackOfficeConfiguration(IFormHandlerSettings settings);
    }
}