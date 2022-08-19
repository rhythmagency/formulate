namespace Formulate.BackOffice.Utilities.Trees
{
    using Microsoft.AspNetCore.Http;

    /// <summary>
    /// Input required get a menu for a tree entity.
    /// </summary>
    public sealed class GetMenuItemsInput
    {
        public GetMenuItemsInput(string id, FormCollection queryStrings)
        {
            Id = id;
            QueryStrings = queryStrings;
        }

        /// <summary>
        /// Gets or sets the Id for the current entity.
        /// </summary>
        public string Id { get; init; }

        /// <summary>
        /// Gets or sets the query strings for the current request.
        /// </summary>
        public FormCollection QueryStrings { get; init; }
    }
}
