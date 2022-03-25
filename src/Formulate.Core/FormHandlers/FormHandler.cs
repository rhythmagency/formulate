namespace Formulate.Core.FormHandlers
{
    // Namespaces.
    using System.Threading;
    using System.Threading.Tasks;

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
        /// Handle the incoming form submission on another thread.
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
        public abstract Task HandleAsync(object submission,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Initializes a new instance of the <see cref="FormHandler"/> class.
        /// </summary>
        /// <param name="settings">The form handler settings.</param>
        protected FormHandler(IFormHandlerSettings settings) : base(settings)
        {
            Enabled = settings.Enabled;
            KindId = settings.KindId;
            Name = settings.Name;
            Alias = settings.Alias;
            Id = settings.Id;
        }
    }
}