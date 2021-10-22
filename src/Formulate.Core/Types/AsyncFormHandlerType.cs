using Formulate.Core.FormHandlers;

namespace Formulate.Core.Types
{
    /// <summary>
    /// An abstract class for creating an async form handler type.
    /// </summary>
    public abstract class AsyncFormHandlerType : FormHandlerTypeBase
    {
        /// <summary>
        /// Creates an Async Form Handler.
        /// </summary>
        /// <returns>A <see cref="AsyncFormHandler"/>.</returns>
        public abstract AsyncFormHandler CreateAsyncHandler();
    }
}
