namespace formulate.app.Models.Requests
{
    public class GetCsvExportRequest
    {

        #region Properties

        /// <summary>
        /// The ID of the form to export to a CSV.
        /// </summary>
        public string FormId { get; set; }

        #endregion

    }
}