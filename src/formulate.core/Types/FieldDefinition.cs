namespace formulate.core.Types
{
    using System;
    using System.Collections.Generic;
    public class FieldDefinition
    {
        public Guid Id { get; set; }
        public IEnumerable<ValidationDefinition> Validations { get; set; }
        public string Alias { get; set; }
        public string Label { get; set; }
        public string Name { get; set; }
        public Type FieldType { get; set; }
        public object Configuration { get; set; }
    }
}