namespace formulate.app.Models.Requests
{
    public class PersistValidationRequest
    {
        public string KindId { get; set; }
        public string ParentId { get; set; }
        public string ValidationId { get; set; }
        public string ValidationName { get; set; }
        public string ValidationAlias { get; set; }
    }
}