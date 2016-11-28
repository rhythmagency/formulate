namespace formulate.app.Models.Requests
{
    public class GetStoredDataRequest
    {

        #region Properties

        /// <summary>
        /// The ID of the form to get data for.
        /// </summary>
        public string FormId { get; set; }


        /// <summary>
        /// The page of data to fetch (starts at 1).
        /// </summary>
        public int Page { get; set; }


        /// <summary>
        /// The number of items each page of data should contain.
        /// </summary>
        public int ItemsPerPage { get; set; }


        /// <summary>
        /// The timezone offset from UTC, in minutes. For California, this might be 480, depending
        /// on daylight saving time.
        /// </summary>
        public int TimezoneOffset { get; set; }

        #endregion

    }
}