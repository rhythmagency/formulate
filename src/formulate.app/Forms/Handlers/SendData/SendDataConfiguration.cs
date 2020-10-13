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
        /// Gets or sets the URL to send data to.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the method (e.g., GET, POST) to use when sending the data.
        /// </summary>
        public string Method { get; set; }

        /// <summary>
        /// Gets or sets the format (e.g., query string, form URL encoded, JSON, XML) to use when
        /// sending the data.
        /// </summary>
        public string TransmissionFormat { get; set; }

        /// <summary>
        /// Gets or sets the fields to use when sending the data.
        /// </summary>
        public IEnumerable<FieldMapping> Fields { get; set; }

        /// <summary>
        /// Gets or sets the function that handles the result of a "Send Data" request.
        /// </summary>
        public IHandleSendDataResult ResultHandler { get; set; }

        #endregion
    }
}
