namespace formulate.app.Forms.Handlers.SendData
{

    // Namespaces.
    using System.Collections.Generic;
    using System.Net;


    /// <summary>
    /// The contextual information available right before data is sent from the Send Data handler.
    /// </summary>
    public class SendingDataContext
    {

        #region Properties

        /// <summary>
        /// The form submission context.
        /// </summary>
        public FormSubmissionContext SubmissionContext { get; set; }


        /// <summary>
        /// The configuration for the Send Data handler. This includes things like the URL the
        /// request is being sent to.
        /// </summary>
        public SendDataConfiguration Configuration { get; set; }


        /// <summary>
        /// The initialized web request. This already contains the URL and a few extra request
        /// properties (e.g., the request method and the user agent).
        /// </summary>
        public HttpWebRequest Request { get; set; }


        /// <summary>
        /// The data to be sent with the web request. For any method not sending the data in
        /// the query string, updating this property will be reflected in the web request.
        /// </summary>
        public IEnumerable<KeyValuePair<string, string>> Data { get; set; }

        #endregion

    }

}