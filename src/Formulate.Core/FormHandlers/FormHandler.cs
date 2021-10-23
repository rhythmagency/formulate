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

        /// <summary>
        /// Initializes a new instance of the <see cref="FormHandler"/> class.
        /// </summary>
        /// <param name="settings">The form handler settings.</param>
        protected FormHandler(IFormHandlerSettings settings) : base(settings)
        {
        }
    }
}
