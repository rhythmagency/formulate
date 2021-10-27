using System;

namespace Formulate.Core.FormHandlers
{
    public sealed class PersistedFormHandler : IFormHandlerSettings
    {
        public Guid Id { get; set; }
        public Guid DefinitionId { get; set; }
        public string Name { get; set; }
        public string Configuration { get; set; }
        public string Alias { get; set; }
        public bool Enabled { get; set; }
    }
}