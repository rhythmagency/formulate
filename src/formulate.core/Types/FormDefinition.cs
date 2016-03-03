namespace formulate.core.Types
{
    using System.Collections.Generic;
    public class FormDefinition
    {
        public string Name { get; set; }
        public string Alias { get; set; }
        public IEnumerable<FieldDefinition> Fields { get; set; }
    }
}