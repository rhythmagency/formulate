namespace Formulate.BackOffice.Utilities.Scaffolding.Forms
{
    using Formulate.BackOffice.Persistence;
    using Formulate.Core.Persistence;
    using System;

    public sealed class CreateFormsScaffoldingEntityInput
    {
        public IPersistedEntity? Parent { get; init; }

        public Guid RootId { get; init; }

        public EntityTypes EntityType { get; init; }
    }
}
