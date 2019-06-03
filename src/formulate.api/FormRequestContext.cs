namespace formulate.api
{

    // Namespaces.
    using System.Web;
    using Umbraco.Core.Models;
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

        #endregion

    }

}