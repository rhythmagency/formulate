namespace Formulate.Core.FormHandlers
{
    /// <summary>
    /// An abstract class for creating an async form handler type.
    /// </summary>
    public abstract class AsyncFormHandlerType : FormHandlerTypeBase
    {
        /// <summary>
        /// Creates an Async Form Handler.
        /// </summary>
        /// <param name="settings">The form handler settings.</param>
        /// <returns>A <see cref="AsyncFormHandler"/>.</returns>
        public abstract AsyncFormHandler CreateAsyncHandler(IFormHandlerSettings settings);
    }
}
