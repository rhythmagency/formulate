namespace Formulate.BackOffice.EditorModels
{
    using Formulate.BackOffice.Persistence;
    using Formulate.Core.Persistence;
    
    public interface IEditorModel : IPersistedEntity
    {
        /// <summary>
        /// Gets the alias.
        /// </summary>
        string Alias { get; }

        /// <summary>
        /// Gets the entity type.
        /// </summary>
        EntityTypes EntityType { get; }
    }
}
