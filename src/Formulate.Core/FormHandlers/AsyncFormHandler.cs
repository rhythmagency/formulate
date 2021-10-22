using System.Threading.Tasks;

namespace Formulate.Core.FormHandlers
{
    /// <summary>
    /// A base class for creating an async form handler.
    /// </summary>
    public abstract class AsyncFormHandler : FormHandlerBase
    {
        /// <summary>
        /// Handle the incoming form submission asynchronously.
        /// </summary>
        /// <param name="submission">The form submission.</param>
        /// <returns>A <see cref="Task"/>.</returns>
        public abstract Task HandleAsync(object submission);
    }
}
