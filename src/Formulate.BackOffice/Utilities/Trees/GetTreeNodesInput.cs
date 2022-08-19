namespace Formulate.BackOffice.Utilities.Trees
{
    using Microsoft.AspNetCore.Http;

    /// <summary>
    /// Input required get tree nodes for a tree entity.
    /// </summary>
    public sealed class GetTreeNodesInput
    {
        public GetTreeNodesInput(string id, FormCollection queryStrings)
        {
            Id = id;
            QueryStrings = queryStrings;
        }

        public string Id { get; init; }

        public FormCollection QueryStrings { get; init; }
    }
}
