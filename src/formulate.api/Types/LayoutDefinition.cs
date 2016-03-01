namespace formulate.api.Types
{
    using System;
    public class LayoutDefinition
    {
        public Guid Id { get; set; }
        public string Alias { get; set; }
        public string Name { get; set; }
        public Type LayoutType { get; set; }
        public object Configuration { get; set; }
    }
}