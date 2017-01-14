namespace formulate.app.Forms.Handlers.SendData
{

    // Namespaces.
    using System.Collections.Generic;


    /// <summary>
    /// Stores the configuration for a "Send Data" form submission handler.
    /// </summary>
    public class SendDataConfiguration
    {

        #region Properties

        /// <summary>
        /// The URL to send data to.
        /// </summary>
        public string Url { get; set; }


        /// <summary>
        /// The method (e.g., GET, POST) to use when sending the data.
        /// </summary>
        public string Method { get; set; }


        /// <summary>
        /// The format (e.g., query string, form URL encoded, JSON, XML) to use when
        /// sending the data.
        /// </summary>
        public string TransmissionFormat { get; set; }


        /// <summary>
        /// The fields to use when sending the data.
        /// </summary>
        public IEnumerable<FieldMapping> Fields { get; set; }


        /// <summary>
        /// The function that handles the result of a "Send Data" request.
        /// </summary>
        public IHandleSendDataResult ResultHandler { get; set; }

        #endregion

    }

}