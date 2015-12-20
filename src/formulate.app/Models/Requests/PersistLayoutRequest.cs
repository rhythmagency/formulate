namespace formulate.app.Models.Requests
{
    public class PersistLayoutRequest
    {
        public string ParentId { get; set; }
        public string LayoutId { get; set; }
        public string LayoutName { get; set; }
        public string LayoutAlias { get; set; }
    }
}