namespace formulate.app.Models.Parameters
{
    public class FieldInfo
    {
        public string Id { get; set; }
        public string Alias { get; set; }
        public string Name { get; set; }
        public string Label { get; set; }
        public string[] Validations { get; set; }
        public string TypeFullName { get; set; }
    }
}