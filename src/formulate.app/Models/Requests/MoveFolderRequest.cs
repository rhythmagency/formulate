namespace formulate.app.Models.Requests
{
    public class MoveFolderRequest
    {
        public string NewParentId { get; set; }
        public string FolderId { get; set; }
    }
}