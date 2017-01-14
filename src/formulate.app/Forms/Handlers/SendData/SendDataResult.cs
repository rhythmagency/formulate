namespace formulate.app.Forms.Handlers.SendData
{

    // Namespaces.
    using System;
    using System.Net;


    /// <summary>
    /// Stores the result of a "Send Data" request.
    /// </summary>
    /// <remarks>
    /// Since there are many types of "Send Data" requests, there are some properties of this
    /// class that may not be populated.
    /// </remarks>
    public class SendDataResult
    {

        #region Properties

        /// <summary>
        /// The HTTP web response, if applicable to this result.
        /// </summary>
        public HttpWebResponse HttpWebResponse { get; set; }


        /// <summary>
        /// The response text, if applicable to this result.
        /// </summary>
        public string ResponseText { get; set; }


        /// <summary>
        /// The response error, if one occurs.
        /// </summary>
        public Exception ResponseError { get; set; }


        /// <summary>
        /// Was the request a success?
        /// </summary>
        public bool Success { get; set; }


        /// <summary>
        /// The form submission context.
        /// </summary>
        public FormSubmissionContext Context { get; set; }

        #endregion

    }

}