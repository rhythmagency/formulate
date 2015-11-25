namespace formulate.app.Trees
{

    // Namespaces.
    using System.Net.Http.Formatting;
    using Umbraco.Core;
    using Umbraco.Web.Models.Trees;
    using Umbraco.Web.Trees;
    using Constants = Umbraco.Core.Constants;

    //TODO: Much to do in this file.
    [Tree("formulate", "formulate", "Formulate", "icon-folder", "icon-folder-open", true, sortOrder: 0)]
    public class FormulateTreeController : TreeController
    {

        protected override MenuItemCollection GetMenuForNode(string id, FormDataCollection queryStrings)
        {
            var menu = new MenuItemCollection();
            var rootId = Constants.System.Root.ToInvariantString();
            if (id.InvariantEquals(rootId))
            {
                var path = "/App_Plugins/formulate/menu-actions/reload.html";
                var menuItem = new MenuItem()
                {
                    Alias = "reload",
                    Icon = "icon-folder",
                    Name = "Reload"
                };
                menuItem.LaunchDialogView(path, "Reload");
                menu.Items.Add(menuItem);
            }
            else
            {
                var path = "/App_Plugins/formulate/menu-actions/add.html";
                var menuItem = new MenuItem()
                {
                    Alias = "add",
                    Icon = "icon-folder",
                    Name = "Add"
                };
                menuItem.LaunchDialogView(path, "Add");
                menu.Items.Add(menuItem);
            }
            return menu;
        }

        protected override TreeNodeCollection GetTreeNodes(string id, FormDataCollection queryStrings)
        {
            var nodes = new TreeNodeCollection();
            var rootId = Constants.System.Root.ToInvariantString();
            if (id.InvariantEquals(rootId))
            {
                var treeNode = this.CreateTreeNode("1", id, queryStrings, "Data Sources", "icon-folder", false);
                nodes.Add(treeNode);
            }
            return nodes;
            //TODO: ...
        }

    }

}