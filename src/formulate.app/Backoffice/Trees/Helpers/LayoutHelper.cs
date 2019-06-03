namespace formulate.app.Backoffice.Trees.Helpers
{

    // Namespaces.
    using core.Extensions;
    using Entities;
    using Folders;
    using formulate.app.Helpers;
    using Layouts;
    using System;
    using System.Collections.Generic;
    using System.Net.Http.Formatting;
    using Umbraco.Web.Models.Trees;
    using Umbraco.Web.Trees;
    using LayoutConstants = formulate.app.Constants.Trees.Layouts;


    /// <summary>
    /// Helps with layouts in the Formulate tree.
    /// </summary>
    internal class LayoutHelper
    {

        #region Properties

        /// <summary>
        /// The tree controller.
        /// </summary>
        private TreeController Tree { get; set; }


        /// <summary>
        /// The folder helper.
        /// </summary>
        private FolderHelper Helper { get; set; }

        private ILocalizationHelper LocalizationHelper { get; set; }

        #endregion


        #region Constructors

        /// <summary>
        /// Primary constructor.
        /// </summary>
        /// <param name="tree">
        /// The tree controller.
        /// </param>
        /// <param name="helper">
        /// The folder helper.
        /// </param>
        public LayoutHelper(TreeController tree, FolderHelper helper, ILocalizationHelper localizationHelper)
        {
            Tree = tree;
            Helper = helper;
            LocalizationHelper = localizationHelper;
        }

        #endregion


        #region Methods


        /// <summary>
        /// Adds the specified layout entities (layout or folder) to
        /// the tree.
        /// </summary>
        /// <param name="nodes">
        /// The collection to add the nodes to.
        /// </param>
        /// <param name="queryStrings">The query strings.</param>
        /// <param name="entities">
        /// The entities (layouts and folders) to add to the tree.
        /// </param>
        public void AddLayoutChildrenToTree(TreeNodeCollection nodes,
            FormDataCollection queryStrings, IEnumerable<IEntity> entities)
        {
            foreach (var entity in entities)
            {

                // Folder or layout?
                if (entity is Folder)
                {
                    var folder = entity as Folder;
                    Helper.AddFolderToTree(nodes, queryStrings, folder,
                        LayoutConstants.GroupIcon);
                }
                else if (entity is Layout)
                {
                    var layout = entity as Layout;
                    AddLayoutToTree(nodes, queryStrings, layout);
                }

            }
        }


        /// <summary>
        /// Adds a layout node to the tree.
        /// </summary>
        /// <param name="nodes">
        /// The node collection to add the layout to.
        /// </param>
        /// <param name="queryStrings">The query strings.</param>
        /// <param name="layout">The layout to add.</param>
        public void AddLayoutToTree(TreeNodeCollection nodes,
            FormDataCollection queryStrings, Layout layout)
        {
            var formatUrl = "/formulate/formulate/editLayout/{0}";
            var layoutId = GuidHelper.GetString(layout.Id);
            var layoutRoute = string.Format(formatUrl, layoutId);
            var layoutName = layout.Name.Fallback("Unnamed");
            var parentId = layout.Path[layout.Path.Length - 2];
            var strParentId = GuidHelper.GetString(parentId);
            var layoutNode = Tree.CreateTreeNode(layoutId,
                strParentId, queryStrings, layoutName,
                LayoutConstants.ItemIcon, false, layoutRoute);
            nodes.Add(layoutNode);
        }


        /// <summary>
        /// Adds the "Delete Layout" action to the layout node.
        /// </summary>
        /// <param name="menu">
        /// The menu to add the action to.
        /// </param>
        public void AddDeleteLayoutAction(MenuItemCollection menu)
        {
            var path = "/App_Plugins/formulate/menu-actions/deleteLayout.html";
            var menuItem = new MenuItem()
            {
                Alias = "deleteLayout",
                Icon = "formulate-delete",
                Name = LocalizationHelper.GetMenuItemName("Delete Layout")
            };
            menuItem.LaunchDialogView(path, "Delete Layout");
            menu.Items.Add(menuItem);
        }


        /// <summary>
        /// Adds the "Move" action to the menu.
        /// </summary>
        /// <param name="menu">
        /// The menu items to add the action to.
        /// </param>
        /// <param name="layout">
        /// The layout.
        /// </param>
        public void AddMoveLayoutAction(MenuItemCollection menu, Layout layout)
        {
            var path = "/App_Plugins/formulate/menu-actions/moveLayout.html";
            var menuItem = new MenuItem()
            {
                Alias = "moveLayout",
                Icon = "formulate-move",
                Name = LocalizationHelper.GetMenuItemName("Move")
            };
            var titleFormat = @"Move ""{0}"" Layout";
            var title = string.Format(titleFormat, layout.Name);
            menuItem.LaunchDialogView(path, title);
            menu.Items.Add(menuItem);
        }


        /// <summary>
        /// Adds the "Create Layout" action with the specified ID used
        /// as the parent for the layout that is created.
        /// </summary>
        /// <param name="menu">
        /// The menu to add the action to.
        /// </param>
        /// <param name="entityId">
        /// The ID of the entity to create the layout under.
        /// If null, the layout will be created at the root.
        /// </param>
        public void AddCreateLayoutAction(MenuItemCollection menu,
            Guid? entityId = null)
        {
            var path = "/App_Plugins/formulate/menu-actions/createLayout.html";
            var menuItem = new MenuItem()
            {
                Alias = "createLayout",
                Icon = "formulate-create",
                Name = LocalizationHelper.GetMenuItemName("Create Layout")
            };
            if (entityId.HasValue)
            {
                path = path + "?under=" + GuidHelper.GetString(entityId.Value);
            }
            menuItem.LaunchDialogView(path, "Create Layout");
            menu.Items.Add(menuItem);
        }

        #endregion

    }

}