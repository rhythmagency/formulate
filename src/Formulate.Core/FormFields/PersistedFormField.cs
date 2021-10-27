using System;

namespace Formulate.Core.FormFields
{
    public sealed class PersistedFormField : IFormFieldSettings
    {
        public Guid Id { get; set; }
        public Guid DefinitionId { get; set; }
        public string Name { get; set; }
        public string Configuration { get; set; }
        public string Alias { get; set; }
        public string Label { get; set; }
        public string Category { get; set; }
        public Guid[] Validations { get; set; }
    }
}