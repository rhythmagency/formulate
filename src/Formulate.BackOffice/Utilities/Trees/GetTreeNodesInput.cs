namespace Formulate.BackOffice.Utilities.Trees
{
    /// <summary>
    /// Input required get tree nodes for a tree entity.
    /// </summary>
    public sealed class GetTreeNodesInput
    {
        public GetTreeNodesInput(string parentId, bool isFoldersOnly)
        {
            ParentId = parentId;
            IsFoldersOnly = isFoldersOnly;
        }

        public string ParentId { get; init; }

        public bool IsFoldersOnly { get; init; }
    }
}
