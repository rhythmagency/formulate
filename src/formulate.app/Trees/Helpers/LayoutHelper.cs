namespace formulate.app.Trees.Helpers
{

    // Namespaces.
    using Entities;
    using Folders;
    using formulate.app.Helpers;
    using Layouts;
    using Persistence;
    using System;
    using System.Collections.Generic;
    using System.Net.Http.Formatting;
    using Umbraco.Web.Models.Trees;
    using Umbraco.Web.Trees;
    using LayoutsConstants = formulate.app.Constants.Trees.Layouts;

    internal class LayoutHelper
    {

        private IEntityPersistence TreeEntityPersistence { get; set; }
        private TreeController Tree { get; set; }
        private FolderHelper Helper { get; set; }


        public LayoutHelper(IEntityPersistence persistence,
            TreeController tree, FolderHelper helper)
        {
            TreeEntityPersistence = persistence;
            Tree = tree;
            Helper = helper;
        }


        public void AddDeleteLayoutAction(MenuItemCollection menu)
        {

            // Configure "Delete Layout" button.
            var path = "/App_Plugins/formulate/menu-actions/deleteLayout.html";
            var menuItem = new MenuItem()
            {
                Alias = "deleteLayout",
                Icon = "folder",
                Name = "Delete Layout"
            };
            menuItem.LaunchDialogView(path, "Delete Layout");
            menu.Items.Add(menuItem);

        }


        public void AddLayoutChildrenToTree(TreeNodeCollection nodes,
            FormDataCollection queryStrings,
            IEnumerable<IEntity> entities)
        {

            // Add folders/layouts to tree.
            foreach (var entity in entities)
            {
                if (entity is Folder)
                {
                    var folder = entity as Folder;
                    Helper.AddFolderToTree(nodes, queryStrings, folder,
                        LayoutsConstants.GroupIcon);
                }
                else if (entity is Layout)
                {
                    var layout = entity as Layout;
                    AddLayoutToTree(nodes, queryStrings, layout);
                }
            }

        }


        public void AddLayoutToTree(TreeNodeCollection nodes,
            FormDataCollection queryStrings, Layout layout)
        {
            var formatUrl = "/formulate/formulate/editLayout/{0}";
            var layoutId = GuidHelper.GetString(layout.Id);
            var layoutRoute = string.Format(formatUrl, layoutId);
            var layoutName = layout.Name ?? "Unnamed";
            var parentId = layout.Path[layout.Path.Length - 2];
            var strParentId = GuidHelper.GetString(parentId);
            var layoutNode = Tree.CreateTreeNode(layoutId,
                strParentId, queryStrings, layoutName,
                LayoutsConstants.ItemIcon, false, layoutRoute);
            nodes.Add(layoutNode);
        }


        public void AddCreateLayoutAction(MenuItemCollection menu,
            Guid? parentId = null)
        {

            // Configure "Create Layout" button.
            var path = "/App_Plugins/formulate/menu-actions/createLayout.html";
            var menuItem = new MenuItem()
            {
                Alias = "createLayout",
                Icon = "folder",
                Name = "Create Layout"
            };
            if (parentId.HasValue)
            {
                path = path + "?under=" + GuidHelper.GetString(parentId.Value);
            }
            menuItem.LaunchDialogView(path, "Create Layout");
            menu.Items.Add(menuItem);

        }

    }
}