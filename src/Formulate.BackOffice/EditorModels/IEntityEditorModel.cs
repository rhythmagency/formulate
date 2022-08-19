namespace Formulate.BackOffice.EditorModels
{

    public interface IEntityEditorModel : IEditorModel
    {
        /// <summary>
        /// Gets the entity type.
        /// </summary>
        EntityTypes EntityType { get; }

        /// <summary>
        /// Gets the tree path.
        /// </summary>
        /// <remarks>This should be the same as the path but in a format safe for an Umbraco tree.</remarks>
        string[] TreePath { get; }
    }
}
