namespace Formulate.BackOffice.Utilities.Trees
{
    using Formulate.BackOffice.Trees;
    using Microsoft.AspNetCore.Http;
    using System.Collections.Generic;
    using Umbraco.Cms.Core.Trees;

    /// <summary>
    /// A base contract for assisting with <see cref="FormulateEntityTreeController"/>
    /// </summary>
    /// <remarks>This should not be used directly in dependency injection it exists to be inherited by other interfaces for specific purposes.</remarks>
    public interface IEntityTreeUtility
    {
        MenuItemCollection GetMenuItems(GetMenuItemsInput input);

        MenuItemCollection GetMenuItemsForRoot(FormCollection queryStrings);
                
        IReadOnlyCollection<EntityTreeNode> GetTreeNodes(GetTreeNodesInput input);
    }
}
