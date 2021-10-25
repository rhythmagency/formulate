namespace Formulate.Core.FormHandlers
{
    /// <summary>
    /// An abstract class for creating an async form handler definition.
    /// </summary>
    public abstract class AsyncFormHandlerDefinition : FormHandlerDefinitionBase
    {
        /// <summary>
        /// Creates an Async Form Handler.
        /// </summary>
        /// <param name="settings">The form handler settings.</param>
        /// <returns>A <see cref="AsyncFormHandler"/>.</returns>
        public abstract AsyncFormHandler CreateAsyncHandler(IFormHandlerSettings settings);
    }
}
