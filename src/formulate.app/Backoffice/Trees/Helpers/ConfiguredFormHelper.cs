namespace formulate.app.Backoffice.Trees.Helpers
{

    // Namespaces.
    using core.Extensions;
    using Entities;
    using Forms;
    using formulate.app.Helpers;
    using System;
    using System.Collections.Generic;
    using System.Net.Http.Formatting;
    using Umbraco.Web.Models.Trees;
    using Umbraco.Web.Trees;
    using ConFormConstants = formulate.app.Constants.Trees.ConfiguredForms;


    /// <summary>
    /// Helps with configured forms in the Formulate tree.
    /// </summary>
    internal class ConfiguredFormHelper
    {

        #region Properties

        /// <summary>
        /// The tree controller.
        /// </summary>
        private TreeController Tree { get; set; }

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
        public ConfiguredFormHelper(TreeController tree, ILocalizationHelper localizationHelper)
        {
            Tree = tree;
            LocalizationHelper = localizationHelper;
        }

        #endregion


        #region Methods


        /// <summary>
        /// Adds the specified configured form entities to the tree.
        /// </summary>
        /// <param name="nodes">
        /// The collection to add the nodes to.
        /// </param>
        /// <param name="queryStrings">The query strings.</param>
        /// <param name="entities">
        /// The entities to add to the tree.
        /// </param>
        public void AddConfiguredFormChildrenToTree(TreeNodeCollection nodes,
            FormDataCollection queryStrings, IEnumerable<IEntity> entities)
        {
            foreach (var entity in entities)
            {
                var conForm = entity as ConfiguredForm;
                AddConfiguredFormToTree(nodes, queryStrings, conForm);
            }
        }


        /// <summary>
        /// Adds a configured form node to the tree.
        /// </summary>
        /// <param name="nodes">
        /// The node collection to add the configured form to.
        /// </param>
        /// <param name="queryStrings">The query strings.</param>
        /// <param name="configuredForm">The configured form to add.</param>
        public void AddConfiguredFormToTree(TreeNodeCollection nodes,
            FormDataCollection queryStrings, ConfiguredForm configuredForm)
        {
            var formatUrl = "/formulate/formulate/editConfiguredForm/{0}";
            var configuredFormId = GuidHelper.GetString(configuredForm.Id);
            var configuredFormRoute = string.Format(formatUrl, configuredFormId);
            var configuredFormName = configuredForm.Name.Fallback("Unnamed");
            var parentId = configuredForm.Path[configuredForm.Path.Length - 2];
            var strParentId = GuidHelper.GetString(parentId);
            var configuredFormNode = Tree.CreateTreeNode(configuredFormId,
                strParentId, queryStrings, configuredFormName,
                ConFormConstants.ItemIcon, false, configuredFormRoute);
            nodes.Add(configuredFormNode);
        }


        /// <summary>
        /// Adds the "Delete Configured Form" action to the configured form node.
        /// </summary>
        /// <param name="menu">
        /// The menu to add the action to.
        /// </param>
        public void AddDeleteAction(MenuItemCollection menu)
        {
            var path = "/App_Plugins/formulate/menu-actions/deleteConfiguredForm.html";
            var menuItem = new MenuItem()
            {
                Alias = "deleteConfiguredForm",
                Icon = "formulate-delete",
                Name = LocalizationHelper.GetMenuItemName("Delete Configuration")
            };
            menuItem.LaunchDialogView(path, "Delete Configuration");
            menu.Items.Add(menuItem);
        }


        /// <summary>
        /// Adds the "Create Configuration" action with the specified ID used
        /// as the parent for the configured form that is created.
        /// </summary>
        /// <param name="menu">
        /// The menu to add the action to.
        /// </param>
        /// <param name="entityId">
        /// The ID of the entity to create the configured form under.
        /// </param>
        public void AddCreateConfiguredFormAction(MenuItemCollection menu,
            Guid entityId)
        {
            var path = "/formulate/formulate/editConfiguredForm/null?under="
                + GuidHelper.GetString(entityId);
            var menuItem = new MenuItem()
            {
                Alias = "createConfiguredForm",
                Icon = "formulate-create",
                Name = LocalizationHelper.GetMenuItemName("Create Configuration")
            };
            menuItem.NavigateToRoute(path);
            menu.Items.Add(menuItem);
        }

        #endregion

    }

}