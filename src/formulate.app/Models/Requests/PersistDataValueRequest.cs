namespace formulate.app.Models.Requests
{
    public class PersistDataValueRequest
    {
        public string KindId { get; set; }
        public string ParentId { get; set; }
        public string DataValueId { get; set; }
        public string DataValueName { get; set; }
        public string DataValueAlias { get; set; }
    }
}