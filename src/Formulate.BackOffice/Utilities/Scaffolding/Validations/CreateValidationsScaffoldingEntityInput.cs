namespace Formulate.BackOffice.Utilities.Validations
{
    using Formulate.Core.Persistence;
    using System;

    public sealed class CreateValidationsScaffoldingEntityInput
    {
        public IPersistedEntity? Parent { get; init; }

        public Guid RootId { get; init; }

        public EntityTypes EntityType { get; init; }
        
        public Guid? KindId { get; init; }
    }
}
