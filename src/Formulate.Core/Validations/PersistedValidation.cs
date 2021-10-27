using System;
using Formulate.Core.Persistence;

namespace Formulate.Core.Validations
{
    public sealed class PersistedValidation : IPersistedEntity, IValidationSettings
    {
        public Guid Id { get; set; }

        public Guid DefinitionId { get; set; }

        public Guid[] Path { get; set; }

        public string Name { get; set; }
        
        
        public string Configuration { get; set; }
    }
}
