namespace formulate.api
{

    // Namespaces.
    using System.Web;
    using Umbraco.Core.Models.PublishedContent;
    using Umbraco.Core.Services;
    using Umbraco.Web;

    /// <summary>
    /// The contextual information used during a request to submit a form.
    /// </summary>
    public class FormRequestContext
    {
        #region Properties

        /// <summary>
        /// Gets or sets the current Umbraco page.
        /// </summary>
        public IPublishedContent CurrentPage { get; set; }

        /// <summary>
        /// Gets or sets the current HTTP Context.
        /// </summary>
        public HttpContextBase HttpContext { get; set; }

        /// <summary>
        /// Gets or sets the Umbraco Services Context.
        /// </summary>
        public ServiceContext Services { get; set; }

        /// <summary>
        /// Gets or sets the Umbraco Helper.
        /// </summary>
        public UmbracoHelper UmbracoHelper { get; set; }

        /// <summary>
        /// Gets or sets the Umbraco Context.
        /// </summary>
        public UmbracoContext UmbracoContext { get; set; }

        #endregion
    }
}
