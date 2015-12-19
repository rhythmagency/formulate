namespace formulate.app.Models.Requests
{
    public class CreateFolderRequest
    {
        public string ParentId { get; set; }
        public string FolderName { get; set; }
    }
}