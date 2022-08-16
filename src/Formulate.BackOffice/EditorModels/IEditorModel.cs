namespace Formulate.BackOffice.EditorModels
{
    using Formulate.Core.Persistence;

    public interface IEditorModel : IPersistedItem
    {
        bool IsNew { get; }
    }
}
