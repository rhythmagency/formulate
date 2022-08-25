namespace Formulate.Core.FormHandlers
{
    using Formulate.Core.Submissions.Requests;
    // Namespaces.



    /// <summary>
    /// A base class for creating a form handler.
    /// </summary>
    public abstract class FormHandler : FormHandlerBase
    {
        protected FormHandler(IFormHandlerSettings settings) : base(settings)
        {
        }

        /// <summary>
        /// Handle the incoming form submission on another thread (though it
        /// can handle a portion of the work on the main thread).
        /// </summary>
        /// <param name="submission">
        /// The form submission.
        /// </param>
        public abstract void Handle(FormSubmissionRequest submission);
    }
}