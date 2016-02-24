namespace formulate.app.Models.Requests
{
    public class PersistConfiguredFormRequest
    {
        public string ParentId { get; set; }
        public string ConFormId { get; set; }
        public string Name { get; set; }
        public string TemplateId { get; set; }
        public string LayoutId { get; set; }
    }
}