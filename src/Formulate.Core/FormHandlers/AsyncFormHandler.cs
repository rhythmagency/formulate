using System.Threading;
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
        /// <param name="cancellationToken">The optional cancellation token to cancel the operation.</param>
        /// <returns>A <see cref="Task"/>.</returns>
        public abstract Task HandleAsync(object submission, CancellationToken cancellationToken = default);
    }
}
