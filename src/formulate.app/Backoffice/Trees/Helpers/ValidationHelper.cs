namespace formulate.app.Backoffice.Trees.Helpers
{

    // Namespaces.
    using core.Extensions;
    using Entities;
    using Folders;
    using formulate.app.Helpers;
    using System;
    using System.Collections.Generic;
    using System.Net.Http.Formatting;
    using Umbraco.Web.Models.Trees;
    using Umbraco.Web.Trees;
    using Validations;
    using ValidationConstants = formulate.app.Constants.Trees.Validations;


    /// <summary>
    /// Helps with validations in the Formulate tree.
    /// </summary>
    internal class ValidationHelper
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
        public ValidationHelper(TreeController tree, FolderHelper helper, ILocalizationHelper localizationHelper)
        {
            Tree = tree;
            Helper = helper;
            LocalizationHelper = localizationHelper;
        }

        #endregion


        #region Methods


        /// <summary>
        /// Adds the specified validation entities (validation or folder)
        /// to the tree.
        /// </summary>
        /// <param name="nodes">
        /// The collection to add the nodes to.
        /// </param>
        /// <param name="queryStrings">The query strings.</param>
        /// <param name="entities">
        /// The entities (validations and folders) to add to the tree.
        /// </param>
        public void AddValidationChildrenToTree(TreeNodeCollection nodes,
            FormDataCollection queryStrings, IEnumerable<IEntity> entities)
        {
            foreach (var entity in entities)
            {

                // Folder or validation?
                if (entity is Folder)
                {
                    var folder = entity as Folder;
                    Helper.AddFolderToTree(nodes, queryStrings, folder,
                        ValidationConstants.GroupIcon);
                }
                else if (entity is Validation)
                {
                    var validation = entity as Validation;
                    AddValidationToTree(nodes, queryStrings, validation);
                }

            }
        }


        /// <summary>
        /// Adds a validation node to the tree.
        /// </summary>
        /// <param name="nodes">
        /// The node collection to add the validation to.
        /// </param>
        /// <param name="queryStrings">The query strings.</param>
        /// <param name="validation">The validation to add.</param>
        public void AddValidationToTree(TreeNodeCollection nodes,
            FormDataCollection queryStrings, Validation validation)
        {
            var formatUrl = "/formulate/formulate/editValidation/{0}";
            var validationId = GuidHelper.GetString(validation.Id);
            var validationRoute = string.Format(formatUrl, validationId);
            var validationName = validation.Name.Fallback("Unnamed");
            var parentId = validation.Path[validation.Path.Length - 2];
            var strParentId = GuidHelper.GetString(parentId);
            var validationNode = Tree.CreateTreeNode(validationId,
                strParentId, queryStrings, validationName,
                ValidationConstants.ItemIcon, false, validationRoute);
            nodes.Add(validationNode);
        }


        /// <summary>
        /// Adds the "Delete Validation" action to the validation node.
        /// </summary>
        /// <param name="menu">
        /// The menu to add the action to.
        /// </param>
        public void AddDeleteValidationAction(MenuItemCollection menu)
        {
            var path = "/App_Plugins/formulate/menu-actions/deleteValidation.html";
            var menuItem = new MenuItem()
            {
                Alias = "deleteValidation",
                Icon = "formulate-delete",
                Name = LocalizationHelper.GetMenuItemName("Delete Validation")
            };
            menuItem.LaunchDialogView(path, "Delete Validation");
            menu.Items.Add(menuItem);
        }


        /// <summary>
        /// Adds the "Move" action to the menu.
        /// </summary>
        /// <param name="menu">
        /// The menu items to add the action to.
        /// </param>
        /// <param name="validation">
        /// The validation.
        /// </param>
        public void AddMoveValidationAction(MenuItemCollection menu, Validation validation)
        {
            var path = "/App_Plugins/formulate/menu-actions/moveValidation.html";
            var menuItem = new MenuItem()
            {
                Alias = "moveValidation",
                Icon = "formulate-move",
                Name = LocalizationHelper.GetMenuItemName("Move")
            };
            var titleFormat = @"Move ""{0}"" Validation";
            var title = string.Format(titleFormat, validation.Name);
            menuItem.LaunchDialogView(path, title);
            menu.Items.Add(menuItem);
        }


        /// <summary>
        /// Adds the "Create Validation" action with the specified ID used
        /// as the parent for the validation that is created.
        /// </summary>
        /// <param name="menu">
        /// The menu to add the action to.
        /// </param>
        /// <param name="entityId">
        /// The ID of the entity to create the validation under.
        /// If null, the validation will be created at the root.
        /// </param>
        public void AddCreateValidationAction(MenuItemCollection menu,
            Guid? entityId = null)
        {
            var path = "/App_Plugins/formulate/menu-actions/createValidation.html";
            var menuItem = new MenuItem()
            {
                Alias = "createValidation",
                Icon = "formulate-create",
                Name = LocalizationHelper.GetMenuItemName("Create Validation")
            };
            if (entityId.HasValue)
            {
                path = path + "?under=" + GuidHelper.GetString(entityId.Value);
            }
            menuItem.LaunchDialogView(path, "Create Validation");
            menu.Items.Add(menuItem);
        }

        #endregion

    }

}