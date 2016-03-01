namespace formulate.api.Types
{
    using System;
    public class ValidationDefinition
    {
        public Guid Id { get; set; }
        public string Alias { get; set; }
        public string Name { get; set; }
        public object Configuration { get; set; }
        public Type ValidationType { get; set; }
    }
}