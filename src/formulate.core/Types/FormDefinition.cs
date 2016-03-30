namespace formulate.core.Types
{
    using System;
    using System.Collections.Generic;
    public class FormDefinition
    {
        public Guid FormId { get; set; }
        public string Name { get; set; }
        public string Alias { get; set; }
        public IEnumerable<FieldDefinition> Fields { get; set; }
    }
}