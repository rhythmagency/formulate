﻿namespace formulate.app.Trees.Helpers
{

    // Namespaces.
    using core.Extensions;
    using Folders;
    using formulate.app.Helpers;
    using Persistence;
    using System.Linq;
    using System.Net.Http.Formatting;
    using Umbraco.Web.Models.Trees;
    using Umbraco.Web.Trees;
    using CoreConstants = Umbraco.Core.Constants;
    using DataSourceConstants = formulate.app.Constants.Trees.DataSources;
    using DataValueConstants = formulate.app.Constants.Trees.DataValues;
    using DataValueHelper = Trees.Helpers.DataValueHelper;
    using FormConstants = formulate.app.Constants.Trees.Forms;
    using LayoutConstants = formulate.app.Constants.Trees.Layouts;
    using LayoutHelper = Trees.Helpers.LayoutHelper;
    using ValidationConstants = formulate.app.Constants.Trees.Validations;
    using ValidationHelper = Trees.Helpers.ValidationHelper;


    /// <summary>
    /// Helps with folders in the Formulate tree.
    /// </summary>
    internal class FolderHelper
    {

        #region Properties

        /// <summary>
        /// The entity persistence.
        /// </summary>
        private IEntityPersistence Persistence { get; set; }


        /// <summary>
        /// The tree controller.
        /// </summary>
        private TreeController Tree { get; set; }

        #endregion


        #region Constructors

        /// <summary>
        /// Primary constructor.
        /// </summary>
        /// <param name="persistence">
        /// The entity persistence.
        /// </param>
        /// <param name="tree">
        /// The tree controller.
        /// </param>
        public FolderHelper(IEntityPersistence persistence,
            TreeController tree)
        {
            Persistence = persistence;
            Tree = tree;
        }

        #endregion


        #region Methods

        /// <summary>
        /// Adds a folder node to the tree.
        /// </summary>
        /// <param name="nodes">The tree nodes to add to.</param>
        /// <param name="queryStrings">The query string.</param>
        /// <param name="folder">The folder.</param>
        /// <param name="icon">The folder's icon.</param>
        public void AddFolderToTree(TreeNodeCollection nodes,
            FormDataCollection queryStrings, Folder folder, string icon)
        {
            var folderFormat = "/formulate/formulate/editFolder/{0}";
            var folderId = GuidHelper.GetString(folder.Id);
            var folderRoute = string.Format(folderFormat, folderId);
            var folderName = folder.Name.Fallback("Unnamed");
            var hasChildren = Persistence
                .RetrieveChildren(folder.Id).Any();
            var parentId = folder.Path[folder.Path.Length - 2];
            var strParentId = GuidHelper.GetString(parentId);
            var folderNode = Tree.CreateTreeNode(folderId,
                strParentId, queryStrings, folderName,
                icon, hasChildren, folderRoute);
            nodes.Add(folderNode);
        }


        /// <summary>
        /// Adds the "Create Folder" action to the folder's menu.
        /// </summary>
        /// <param name="menu">
        /// The menu items.
        /// </param>
        public void AddCreateFolderAction(MenuItemCollection menu, string sectionId = "")
        {
            var folderIcon = string.Empty;
            switch (sectionId)
            {
                case FormConstants.Id:
                    folderIcon = "formulate-form-group";
                    break;
                case LayoutConstants.Id:
                    folderIcon = "formulate-layout-group";
                    break;
                case DataValueConstants.Id:
                    folderIcon = "formulate-value-group";
                    break;
                case ValidationConstants.Id:
                    folderIcon = "formulate-validation-group";
                    break;
                default:
                    folderIcon = "folder";
                    break;
            }
            var path = "/App_Plugins/formulate/menu-actions/createFolder.html";
            var menuItem = new MenuItem()
            {
                Alias = "createFolder",
                Icon = folderIcon,
                Name = LocalizationHelper.GetMenuItemName("Create Folder")
            };
            menuItem.LaunchDialogView(path, "Create Folder");
            menu.Items.Add(menuItem);
        }


        /// <summary>
        /// Adds the "Move" action to the menu.
        /// </summary>
        /// <param name="menu">
        /// The menu items to add the action to.
        /// </param>
        /// <param name="folder">
        /// The folder.
        /// </param>
        public void AddMoveFolderAction(MenuItemCollection menu, Folder folder)
        {
            var path = "/App_Plugins/formulate/menu-actions/moveFolder.html";
            var menuItem = new MenuItem()
            {
                Alias = "moveFolder",
                Icon = "enter",
                Name = LocalizationHelper.GetMenuItemName("Move")
            };
            var titleFormat = @"Move ""{0}"" Folder";
            var title = string.Format(titleFormat, folder.Name);
            menuItem.LaunchDialogView(path, title);
            menu.Items.Add(menuItem);
        }


        /// <summary>
        /// Adds the "Delete Form" action to the menu.
        /// </summary>
        /// <param name="menu">
        /// The menu items to add the action to.
        /// </param>
        public void AddDeleteFolderAction(MenuItemCollection menu)
        {
            var path = "/App_Plugins/formulate/menu-actions/deleteFolder.html";
            var menuItem = new MenuItem()
            {
                Alias = "deleteFolder",
                Icon = "delete",
                Name = LocalizationHelper.GetMenuItemName("Delete Folder")
            };
            menuItem.LaunchDialogView(path, "Delete Folder");
            menu.Items.Add(menuItem);
        }

        #endregion

    }

}