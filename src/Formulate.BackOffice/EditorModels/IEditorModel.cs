namespace Formulate.BackOffice.EditorModels
{
    using Formulate.Core.Persistence;
    
    public interface IEditorModel : IPersistedEntity
    {
        /// <summary>
        /// Gets the alias.
        /// </summary>
        string Alias { get; }
    }
}
