namespace formulate.app.Forms
{

    // Namespaces.
    using core.Types;
    using System;
    using System.Collections.Generic;
    using System.Web;
    using Umbraco.Core.Models;
    using Umbraco.Core.Models.PublishedContent;
    using Umbraco.Core.Services;
    using Umbraco.Web;


    /// <summary>
    /// The contextual information available during a form submission.
    /// </summary>
    public class FormSubmissionContext
    {

        #region Properties

        /// <summary>
        /// The form being submitted.
        /// </summary>
        public Form Form { get; set; }


        /// <summary>
        /// The data being submitted.
        /// </summary>
        public IEnumerable<FieldSubmission> Data { get; set; }


        /// <summary>
        /// The files being submitted.
        /// </summary>
        public IEnumerable<FileFieldSubmission> Files { get; set; }


        /// <summary>
        /// Extra data related to the submission.
        /// </summary>
        public IEnumerable<PayloadSubmission> Payload { get; set; }


        /// <summary>
        /// The current Umbraco page.
        /// </summary>
        public IPublishedContent CurrentPage { get; set; }


        /// <summary>
        /// The current HTTP context.
        /// </summary>
        public HttpContextBase HttpContext { get; set; }


        /// <summary>
        /// The Umbraco services.
        /// </summary>
        public ServiceContext Services { get; set; }


        /// <summary>
        /// The Umbraco helper.
        /// </summary>
        public UmbracoHelper UmbracoHelper { get; set; }


        /// <summary>
        /// The Umbraco context.
        /// </summary>
        public UmbracoContext UmbracoContext { get; set; }


        /// <summary>
        /// A generated ID that can be used to track a submission.
        /// </summary>
        public Guid SubmissionId { get; set; }


        /// <summary>
        /// Collection of multi-purpose contextual data.
        /// </summary>
        public Dictionary<string, object> ExtraContext { get; set; }


        /// <summary>
        /// Flag to allow this form to be cancelled by the event handler.
        /// </summary>
        public bool SubmissionCancelled { get; set; }

        #endregion

    }

}