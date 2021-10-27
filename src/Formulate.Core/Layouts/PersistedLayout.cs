using System;
using Formulate.Core.Persistence;

namespace Formulate.Core.Layouts
{
    public sealed class PersistedLayout : IPersistedEntity, ILayoutSettings
    {
        public Guid Id { get; set; }
        
        public Guid DefinitionId { get; set; }
        
        public Guid[] Path { get; set; }
        
        public string Name { get; set; }
        
        public string Configuration { get; set; }
        
        public string Directive { get; set; }
    }
}
