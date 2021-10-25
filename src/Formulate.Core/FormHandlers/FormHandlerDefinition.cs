namespace Formulate.Core.FormHandlers
{
    /// <summary>
    /// An abstract class for creating a form handler definition.
    /// </summary>
    public abstract class FormHandlerDefinition : FormHandlerDefinitionBase
    {
        /// <summary>
        /// Creates a Form Handler.
        /// </summary>
        /// <param name="settings">The form handler settings.</param>
        /// <returns>A <see cref="FormHandler"/>.</returns>
        public abstract FormHandler CreateHandler(IFormHandlerSettings settings);
    }
}