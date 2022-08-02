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

        /// <summary>
        /// Gets the tree path.
        /// </summary>
        /// <remarks>This should be the same as the path but in a format safe for an Umbraco tree.</remarks>
        string[] TreePath { get; }


        bool IsNew { get; }
    }
}
