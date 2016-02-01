namespace formulate.app.Models.Requests
{
    public class PersistFolderRequest
    {
        public string FolderId { get; set; }
        public string ParentId { get; set; }
        public string FolderName { get; set; }
    }
}