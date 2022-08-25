namespace Formulate.Core.FormHandlers
{
    using Formulate.Core.Submissions.Requests;
    // Namespaces.
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// A base class for creating an async form handler.
    /// </summary>
    public abstract class AsyncFormHandler : FormHandlerBase
    {
        protected AsyncFormHandler(IFormHandlerSettings settings) : base(settings)
        {
        }

        /// <summary>
        /// Handle the incoming form submission on another thread (though it
        /// can handle a portion of the work on the main thread).
        /// </summary>
        /// <param name="submission">
        /// The form submission.
        /// </param>
        /// <param name="cancellationToken">
        /// Optional. Used to cancel the async operation.
        /// </param>
        /// <returns>
        /// The async task.
        /// </returns>
        public abstract Task HandleAsync(FormSubmissionRequest submission,
            CancellationToken cancellationToken = default);

    }
}