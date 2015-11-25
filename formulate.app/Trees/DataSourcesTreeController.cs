namespace formulate.app.Trees
{

    // Namespaces.
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Umbraco.Web.Trees;
    using Umbraco.Web.Models.Trees;
    using System.Net.Http.Formatting;
    using Constants = Umbraco.Core.Constants;
    using Umbraco.Core;


    //TODO: Much to do in this file.
    public class DataSourcesTreeController : TreeController
    {

        // A menu is what appears when you right-click a node.
        protected override MenuItemCollection GetMenuForNode(string id, FormDataCollection queryStrings)
        {
            var menu = new MenuItemCollection();
            var rootId = Constants.System.Root.ToInvariantString();
            if (id.InvariantEquals(rootId))
            {

            }
            else
            {
            }
            //TODO: ...
            return new MenuItemCollection();
        }

        // Tree nodes are what appear in the tree.
        protected override TreeNodeCollection GetTreeNodes(string id, FormDataCollection queryStrings)
        {
            return new TreeNodeCollection();
            //TODO: ...
        }


        protected override TreeNode CreateRootNode(FormDataCollection queryStrings)
        {
            var rootNode = base.CreateRootNode(queryStrings);
            rootNode.Name = "Testing Name";
            return rootNode;
        }

    }

}