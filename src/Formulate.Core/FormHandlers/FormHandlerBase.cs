namespace Formulate.Core.FormHandlers
{
    // Namespaces.
    using System;

    /// <summary>
    /// The base class for all form handlers.
    /// </summary>
    /// <remarks>Do not implement this definition directly. Instead implement <see cref="FormHandler"/> or <see cref="AsyncFormHandler"/>.</remarks>
    public abstract class FormHandlerBase : IFormHandler
    {
        /// <summary>
        /// Gets or sets the ID of the type of form handler this is.
        /// </summary>
        public Guid KindId { get; set; }

        /// <summary>
        /// Gets or sets the ID for this form handler.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the alias for this form handler.
        /// </summary>
        public string Alias { get; set; }

        /// <summary>
        /// Gets or sets the name for this form handler.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether or not this
        /// form handler is enabled.
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// Gets or sets the icon for this form handler.
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// Gets or sets the AngularJS directive for this form handler.
        /// </summary>
        public string Directive { get; set; }

        /// <summary>
        /// The raw configuration.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This is for reference only.
        /// </para>
        /// <para>
        /// Deserialization should typically happen in the overridden <see cref="FormHandlerDefinition"/> CreateHandler or the <see cref="AsyncFormHandler"/> CreateAsyncHandler methods.
        /// </para>
        /// </remarks>
        protected readonly string RawConfiguration;

        /// <summary>
        /// Initializes a new instance of the <see cref="FormHandlerBase"/> class.
        /// </summary>
        /// <param name="settings">The form handler settings.</param>
        /// <exception cref="ArgumentNullException">The settings parameter is null.</exception>
        protected FormHandlerBase(IFormHandlerSettings settings)
        {
            if (settings is null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            Id = settings.Id;
            KindId = settings.Id;
            Alias = settings.Alias;
            Name = settings.Name;
            Enabled = settings.Enabled;
            RawConfiguration = settings.Data;
        }
    }
}