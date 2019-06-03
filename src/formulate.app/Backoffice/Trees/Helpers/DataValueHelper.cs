namespace formulate.app.Backoffice.Trees.Helpers
{

    // Namespaces.
    using core.Extensions;
    using DataValues;
    using Entities;
    using Folders;
    using formulate.app.Helpers;
    using System;
    using System.Collections.Generic;
    using System.Net.Http.Formatting;
    using Umbraco.Web.Models.Trees;
    using Umbraco.Web.Trees;
    using DataValueConstants = formulate.app.Constants.Trees.DataValues;


    /// <summary>
    /// Helps with data values in the Formulate tree.
    /// </summary>
    internal class DataValueHelper
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
        public DataValueHelper(TreeController tree, FolderHelper helper, ILocalizationHelper localizationHelper)
        {
            Tree = tree;
            Helper = helper;
            LocalizationHelper = localizationHelper;
        }

        #endregion


        #region Methods


        /// <summary>
        /// Adds the specified data value entities (data value or folder)
        /// to the tree.
        /// </summary>
        /// <param name="nodes">
        /// The collection to add the nodes to.
        /// </param>
        /// <param name="queryStrings">The query strings.</param>
        /// <param name="entities">
        /// The entities (data values and folders) to add to the tree.
        /// </param>
        public void AddChildrenToTree(TreeNodeCollection nodes,
            FormDataCollection queryStrings, IEnumerable<IEntity> entities)
        {
            foreach (var entity in entities)
            {

                // Folder or data value?
                if (entity is Folder)
                {
                    var folder = entity as Folder;
                    Helper.AddFolderToTree(nodes, queryStrings, folder,
                        DataValueConstants.GroupIcon);
                }
                else if (entity is DataValue)
                {
                    var dataValue = entity as DataValue;
                    AddToTree(nodes, queryStrings, dataValue);
                }

            }
        }


        /// <summary>
        /// Adds a data value node to the tree.
        /// </summary>
        /// <param name="nodes">
        /// The node collection to add the data value to.
        /// </param>
        /// <param name="queryStrings">The query strings.</param>
        /// <param name="dataValue">The data value to add.</param>
        public void AddToTree(TreeNodeCollection nodes,
            FormDataCollection queryStrings, DataValue dataValue)
        {
            var formatUrl = "/formulate/formulate/editDataValue/{0}";
            var dataValueId = GuidHelper.GetString(dataValue.Id);
            var dataValueRoute = string.Format(formatUrl, dataValueId);
            var dataValueName = dataValue.Name.Fallback("Unnamed");
            var parentId = dataValue.Path[dataValue.Path.Length - 2];
            var strParentId = GuidHelper.GetString(parentId);
            var dataValueNode = Tree.CreateTreeNode(dataValueId,
                strParentId, queryStrings, dataValueName,
                DataValueConstants.ItemIcon, false, dataValueRoute);
            nodes.Add(dataValueNode);
        }


        /// <summary>
        /// Adds the "Delete Data Value" action to the data value node.
        /// </summary>
        /// <param name="menu">
        /// The menu to add the action to.
        /// </param>
        public void AddDeleteAction(MenuItemCollection menu)
        {
            var path = "/App_Plugins/formulate/menu-actions/deleteDataValue.html";
            var menuItem = new MenuItem()
            {
                Alias = "deleteDataValue",
                Icon = "formulate-delete",
                Name = LocalizationHelper.GetMenuItemName("Delete Data Value")
            };
            menuItem.LaunchDialogView(path, "Delete Data Value");
            menu.Items.Add(menuItem);
        }


        /// <summary>
        /// Adds the "Move" action to the menu.
        /// </summary>
        /// <param name="menu">
        /// The menu items to add the action to.
        /// </param>
        /// <param name="dataValue">
        /// The data value.
        /// </param>
        public void AddMoveDataValueAction(MenuItemCollection menu, DataValue dataValue)
        {
            var path = "/App_Plugins/formulate/menu-actions/moveDataValue.html";
            var menuItem = new MenuItem()
            {
                Alias = "moveDataValue",
                Icon = "formulate-move",
                Name = LocalizationHelper.GetMenuItemName("Move")
            };
            var titleFormat = @"Move ""{0}"" Data Value";
            var title = string.Format(titleFormat, dataValue.Name);
            menuItem.LaunchDialogView(path, title);
            menu.Items.Add(menuItem);
        }


        /// <summary>
        /// Adds the "Create Data Value" action with the specified ID used
        /// as the parent for the data value that is created.
        /// </summary>
        /// <param name="menu">
        /// The menu to add the action to.
        /// </param>
        /// <param name="entityId">
        /// The ID of the entity to create the data value under.
        /// If null, the data value will be created at the root.
        /// </param>
        public void AddCreateAction(MenuItemCollection menu,
            Guid? entityId = null)
        {
            var path = "/App_Plugins/formulate/menu-actions/createDataValue.html";
            var menuItem = new MenuItem()
            {
                Alias = "createDataValue",
                Icon = "formulate-create",
                Name = LocalizationHelper.GetMenuItemName("Create Data Value")
            };
            if (entityId.HasValue)
            {
                path = path + "?under=" + GuidHelper.GetString(entityId.Value);
            }
            menuItem.LaunchDialogView(path, "Create Data Value");
            menu.Items.Add(menuItem);
        }

        #endregion

    }

}