namespace formulate.app.Forms
{

    // Namespaces.
    using System;
    using System.Collections.Generic;
    using System.Web;

    using core.Types;

    using Umbraco.Core.Models.PublishedContent;
    using Umbraco.Core.Services;
    using Umbraco.Web;

    /// <summary>
    /// Gets or sets the contextual information available during a form submission.
    /// </summary>
    public class FormSubmissionContext
    {
        #region Properties

        /// <summary>
        /// Gets or sets the form being submitted.
        /// </summary>
        public Form Form { get; set; }

        /// <summary>
        /// Gets or sets the data being submitted.
        /// </summary>
        public IEnumerable<FieldSubmission> Data { get; set; }

        /// <summary>
        /// Gets or sets the files being submitted.
        /// </summary>
        public IEnumerable<FileFieldSubmission> Files { get; set; }

        /// <summary>
        /// Gets or sets extra data related to the submission.
        /// </summary>
        public IEnumerable<PayloadSubmission> Payload { get; set; }

        /// <summary>
        /// Gets or sets the current Umbraco page.
        /// </summary>
        public IPublishedContent CurrentPage { get; set; }

        /// <summary>
        /// Gets or sets the current HTTP context.
        /// </summary>
        public HttpContextBase HttpContext { get; set; }

        /// <summary>
        /// Gets or sets the Umbraco services.
        /// </summary>
        public ServiceContext Services { get; set; }

        /// <summary>
        /// Gets or sets the Umbraco helper.
        /// </summary>
        public UmbracoHelper UmbracoHelper { get; set; }

        /// <summary>
        /// Gets or sets the Umbraco context.
        /// </summary>
        public UmbracoContext UmbracoContext { get; set; }

        /// <summary>
        /// Gets or sets a generated ID that can be used to track a submission.
        /// </summary>
        public Guid SubmissionId { get; set; }

        /// <summary>
        /// Collection of multi-purpose contextual data.
        /// </summary>
        public Dictionary<string, object> ExtraContext { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether or not to allow this form to be cancelled by the event handler.
        /// </summary>
        public bool SubmissionCancelled { get; set; }

        #endregion
    }
}
