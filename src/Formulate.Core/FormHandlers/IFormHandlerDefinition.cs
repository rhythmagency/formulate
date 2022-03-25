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
        /// Creates a Form Handler.
        /// </summary>
        /// <param name="settings">The form handler settings.</param>
        /// <returns>A <see cref="FormHandler"/>.</returns>
        FormHandler CreateHandler(IFormHandlerSettings settings);
    }
}