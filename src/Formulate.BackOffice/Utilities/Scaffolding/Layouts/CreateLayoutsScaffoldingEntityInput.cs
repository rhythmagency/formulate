namespace Formulate.BackOffice.Utilities.Scaffolding.Layouts
{
    using Formulate.Core.Persistence;
    using System;

    public sealed class CreateLayoutsScaffoldingEntityInput
    {
        public IPersistedEntity? Parent { get; init; }

        public Guid RootId { get; init; }

        public EntityTypes EntityType { get; init; }

        public Guid? KindId { get; init; }
    }
}
