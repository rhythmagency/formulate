namespace Formulate.Core.FormHandlers
{
    /// <summary>
    /// A base class for creating a form handler.
    /// </summary>
    public abstract class FormHandler : FormHandlerBase
    {
        /// <summary>
        /// Handle the incoming form submission.
        /// </summary>
        /// <param name="submission">The form submission.</param>
        public abstract void Handle(object submission);
    }
}