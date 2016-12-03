namespace formulate.app.Models.Requests
{
    public class GetFileDownloadRequest
    {

        #region Properties

        /// <summary>
        /// The path segment used to store the file.
        /// </summary>
        public string PathSegment { get; set; }


        /// <summary>
        /// The filename to use when downloading the file.
        /// </summary>
        public string Filename { get; set; }

        #endregion

    }
}