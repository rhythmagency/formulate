namespace formulate.app.Forms
{
    // Namespaces.
    using System;

    /// <summary>
    /// A contract for all form handlers.
    /// </summary>
    public interface IFormHandler
    {
        /// <summary>
        /// Gets or sets the type id.
        /// </summary>
        Guid TypeId { get; set; }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the alias.
        /// </summary>
        string Alias { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this is enabled.
        /// </summary>
        bool Enabled { get; set; }

        /// <summary>
        /// Gets or sets the handler configuration.
        /// </summary>
        string HandlerConfiguration { get; set; }

        /// <summary>
        /// The get icon.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        string GetIcon();

        /// <summary>
        /// The get directive.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        string GetDirective();

        /// <summary>
        /// The get type label.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        string GetTypeLabel();

        /// <summary>
        /// The get handler type.
        /// </summary>
        /// <returns>
        /// The <see cref="Type"/>.
        /// </returns>
        Type GetHandlerType();

        /// <summary>
        /// The deserialize configuration.
        /// </summary>
        /// <returns>
        /// The <see cref="object"/>.
        /// </returns>
        object DeserializeConfiguration();

        /// <summary>
        /// Prepare handle form.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        void PrepareHandleForm(FormSubmissionContext context);

        /// <summary>
        /// Handle form.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        void HandleForm(FormSubmissionContext context);

        /// <summary>
        /// Get fresh copy.
        /// </summary>
        /// <returns>
        /// The <see cref="IFormHandler"/>.
        /// </returns>
        IFormHandler GetFreshCopy();
    }
}
