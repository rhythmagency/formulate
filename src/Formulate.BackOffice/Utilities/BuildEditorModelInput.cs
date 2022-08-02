namespace Formulate.BackOffice.Utilities
{
    using Formulate.Core.Persistence;

    public sealed class BuildEditorModelInput
    {
        public BuildEditorModelInput(IPersistedEntity entity) : this(entity, false)
        {
        }

        public BuildEditorModelInput(IPersistedEntity entity, bool isNew)
        {
            Entity = entity;
            IsNew = isNew;
        }

        public IPersistedEntity Entity { get; init; }

        public bool IsNew { get; init; }
    }
}