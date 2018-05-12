namespace formulate.app.Models.Requests
{
    public class DeleteSubmissionRequest
    {

        #region Properties

        /// <summary>
        /// The generated ID of the form submission to delete.
        /// </summary>
        public string GeneratedId { get; set; }

        #endregion

    }
}