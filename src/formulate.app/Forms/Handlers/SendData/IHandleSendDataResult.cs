namespace formulate.app.Forms.Handlers.SendData
{

    /// <summary>
    /// Classes that implement this interface can handle the result of a "Send Data" handler
    /// request to a web API.
    /// </summary>
    public interface IHandleSendDataResult
    {
        #region Properties

        /// <summary>
        /// Gets the name of this handler.
        /// </summary>
        string Name { get; }

        #endregion


        #region Methods

        /// <summary>
        /// This function is called at the end of a "Send Data" request.
        /// </summary>
        /// <param name="result">
        /// Any information available about the result of the "Send Data" request.
        /// </param>
        void HandleResult(SendDataResult result);

        #endregion
    }
}
