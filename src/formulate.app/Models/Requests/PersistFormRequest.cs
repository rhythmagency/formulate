namespace formulate.app.Models.Requests
{
    using Parameters;
    public class PersistFormRequest
    {
        public string FormId { get; set; }
        public string Alias { get; set; }
        public string Name { get; set; }
        public FieldInfo[] Fields { get; set; }
    }
}