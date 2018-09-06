namespace formulate.app.Models.Parameters
{
    public class HandlerInfo
    {
        public string Id { get; set; }
        public string Alias { get; set; }
        public string Name { get; set; }
        public bool Enabled { get; set; }
        public string TypeFullName { get; set; }
        public object Configuration { get; set; }
    }
}