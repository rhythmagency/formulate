namespace Formulate.BackOffice.Utilities
{
    using Formulate.Core.Persistence;

    public sealed class MapToEditorModelInput
    {
        public MapToEditorModelInput(IPersistedItem item) : this(item, false)
        {
        }

        public MapToEditorModelInput(IPersistedItem item, bool isNew)
        {
            Item = item;
            IsNew = isNew;
        }

        public IPersistedItem Item { get; init; }

        public bool IsNew { get; init; }
    }
}