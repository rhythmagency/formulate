namespace Formulate.BackOffice.Utilities.DataValues
{
    using Formulate.BackOffice.Persistence;
    using Formulate.Core.Persistence;
    using System;

    public sealed class CreateDataValuesScaffoldingEntityInput
    {
        public IPersistedEntity? Parent { get; init; }

        public Guid RootId { get; init; }

        public EntityTypes EntityType { get; init; }
        
        public Guid? KindId { get; init; }
    }
}
