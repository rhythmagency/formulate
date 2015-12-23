namespace formulate.app.Trees
{

    // Namespaces.
    using Folders;
    using Forms;
    using formulate.app.Helpers;
    using formulate.app.Trees.Helpers;
    using Layouts;
    using Persistence;
    using Resolvers;
    using System;
    using System.Linq;
    using System.Net.Http.Formatting;
    using Umbraco.Core;
    using Umbraco.Web.Models.Trees;
    using Umbraco.Web.Mvc;
    using Umbraco.Web.Trees;
    using CoreConstants = Umbraco.Core.Constants;
    using DataSourcesConstants = formulate.app.Constants.Trees.DataSources;
    using DataValuesConstants = formulate.app.Constants.Trees.DataValues;
    using FormsConstants = formulate.app.Constants.Trees.Forms;
    using LayoutsConstants = formulate.app.Constants.Trees.Layouts;
    using ValidationsConstants = formulate.app.Constants.Trees.ValidationLibrary;


    //TODO: Much to do in this file.
    [Tree("formulate", "formulate", "Formulate", "icon-folder", "icon-folder-open", true, sortOrder: 0)]
    [PluginController("formulate")]
    public class FormulateTreeController : TreeController
    {

        private IEntityPersistence Persistence { get; set; }
        private FolderHelper FolderHelper { get; set; }
        private FormHelper FormHelper { get; set; }
        private LayoutHelper LayoutHelper { get; set; }


        public FormulateTreeController()
        {
            Persistence = EntityPersistence.Current.Manager;
            FolderHelper = new FolderHelper(Persistence, this);
            FormHelper = new FormHelper(Persistence, this,
                FolderHelper);
            LayoutHelper = new LayoutHelper(Persistence, this,
                FolderHelper);
        }

        protected override MenuItemCollection GetMenuForNode(string id,
            FormDataCollection queryStrings)
        {
            var menu = new MenuItemCollection();
            var rootId = CoreConstants.System.Root.ToInvariantString();
            var rootFormsId = GuidHelper.GetGuid(FormsConstants.Id);
            var rootLayoutId = GuidHelper.GetGuid(LayoutsConstants.Id);
            if (id.InvariantEquals(rootId))
            {

                // Do nothing. The root requires no actions.

            }
            else if (id.InvariantEquals(FormsConstants.Id))
            {

                // Actions for forms.
                FormHelper.AddCreateFormAction(menu);
                FolderHelper.AddCreateFolderAction(menu);

            }
            else if (id.InvariantEquals(LayoutsConstants.Id))
            {

                // Actions for layouts.
                LayoutHelper.AddCreateLayoutAction(menu);
                FolderHelper.AddCreateFolderAction(menu);

            }
            else
            {

                // Variables.
                var entityId = GuidHelper.GetGuid(id);
                var entity = Persistence.Retrieve(entityId);
                var ancestorId = entity == null
                    ? Guid.Empty
                    : entity.Path.First();


                // What type of entity does the node represent?
                if (entity is Form)
                {
                    FormHelper.AddDeleteFormAction(menu);
                }
                else if (entity is Layout)
                {
                    LayoutHelper.AddDeleteLayoutAction(menu);
                }
                else if (entity is Folder)
                {
                    FolderHelper.AddCreateFolderAction(menu);
                    if (ancestorId == rootFormsId)
                    {
                        FormHelper.AddCreateFormAction(menu, entityId);
                    }
                    else if (ancestorId == rootLayoutId)
                    {
                        LayoutHelper.AddCreateLayoutAction(menu, entityId);
                    }
                }

            }
            return menu;
        }

        protected override TreeNodeCollection GetTreeNodes(string id,
            FormDataCollection queryStrings)
        {
            var entityId = GuidHelper.GetGuid(id);
            var nodes = new TreeNodeCollection();
            var rootId = CoreConstants.System.Root.ToInvariantString();
            var rootFormsId = GuidHelper.GetGuid(FormsConstants.Id);
            var rootLayoutsId = GuidHelper.GetGuid(LayoutsConstants.Id);
            if (id.InvariantEquals(rootId))
            {

                // Get root nodes.
                var formatUrl = "/formulate/formulate/{0}/info";
                var hasRootForms = Persistence
                    .RetrieveChildren(rootFormsId).Any();
                var formsNode = this.CreateTreeNode(FormsConstants.Id, id,
                    queryStrings, FormsConstants.Title, FormsConstants.TreeIcon,
                    hasRootForms,
                    string.Format(formatUrl, "forms"));
                nodes.Add(formsNode);
                var hasRootLayouts = Persistence
                    .RetrieveChildren(rootLayoutsId).Any();
                var layoutsNode = this.CreateTreeNode(LayoutsConstants.Id,
                    id, queryStrings, LayoutsConstants.Title,
                    LayoutsConstants.TreeIcon, hasRootLayouts,
                    string.Format(formatUrl, "layouts"));
                nodes.Add(layoutsNode);
                var dataSourcesNode = this.CreateTreeNode(
                    DataSourcesConstants.Id, id, queryStrings,
                    DataSourcesConstants.Title, DataSourcesConstants.Icon,
                    false, string.Format(formatUrl, "dataSources"));
                nodes.Add(dataSourcesNode);
                var dataValuesNode = this.CreateTreeNode(
                    DataValuesConstants.Id, id, queryStrings,
                    DataValuesConstants.Title, DataValuesConstants.Icon,
                    false, string.Format(formatUrl, "dataValues"));
                nodes.Add(dataValuesNode);
                var validationsNode = this.CreateTreeNode(
                    ValidationsConstants.Id, id, queryStrings,
                    ValidationsConstants.Title, ValidationsConstants.Icon,
                    false, string.Format(formatUrl, "validationLibrary"));
                nodes.Add(validationsNode);

            }
            else if (id.InvariantEquals(LayoutsConstants.Id))
            {

                // Get root nodes under layouts.
                var entities = Persistence
                    .RetrieveChildren(rootLayoutsId);
                LayoutHelper.AddLayoutChildrenToTree(nodes, queryStrings, entities);

            }
            else if (id.InvariantEquals(FormsConstants.Id))
            {

                // Get root nodes under forms.
                var entities = Persistence
                    .RetrieveChildren(rootFormsId);
                FormHelper.AddFormChildrenToTree(nodes, queryStrings, entities);

            }
            else
            {

                // Variables.
                var entity = Persistence.Retrieve(entityId);
                var ancestorId = entity.Path.First();
                var children = Persistence
                    .RetrieveChildren(entityId);


                // Add children of folder.
                if (entity is Folder)
                {

                    // Which subtree?
                    if (ancestorId == rootFormsId)
                    {
                        FormHelper.AddFormChildrenToTree(nodes, queryStrings,
                            children);
                    }
                    else if (ancestorId == rootLayoutsId)
                    {
                        LayoutHelper.AddLayoutChildrenToTree(nodes, queryStrings,
                            children);
                    }

                }

            }
            return nodes;
        }


    }

}