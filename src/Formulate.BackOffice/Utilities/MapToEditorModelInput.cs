namespace Formulate.BackOffice.Utilities
{
    using Formulate.Core.Persistence;

    public sealed class MapToEditorModelInput
    {
        public MapToEditorModelInput(IPersistedEntity entity) : this(entity, false)
        {
        }

        public MapToEditorModelInput(IPersistedEntity entity, bool isNew)
        {
            Entity = entity;
            IsNew = isNew;
        }

        public IPersistedEntity Entity { get; init; }

        public bool IsNew { get; init; }
    }
}