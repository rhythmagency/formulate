namespace formulate.app.Trees
{

    // Namespaces.
    using Entities;
    using Folders;
    using Forms;
    using Helpers;
    using Layouts;
    using Persistence;
    using Resolvers;
    using System;
    using System.Collections.Generic;
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

        private ILayoutPersistence TreeLayoutPersistence { get; set; }
        private IFormPersistence TreeFormPersistence { get; set; }
        private IEntityPersistence TreeEntityPersistence { get; set; }
        private IFolderPersistence TreeFolderPersistence { get; set; }


        public FormulateTreeController()
        {
            TreeLayoutPersistence = LayoutPersistence.Current.Manager;
            TreeFormPersistence = FormPersistence.Current.Manager;
            TreeEntityPersistence = EntityPersistence.Current.Manager;
            TreeFolderPersistence = FolderPersistence.Current.Manager;
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
                var path = "/App_Plugins/formulate/menu-actions/reload.html";
                var menuItem = new MenuItem()
                {
                    Alias = "reload",
                    Icon = "folder",
                    Name = "Reload"
                };
                menuItem.LaunchDialogView(path, "Reload");
                menu.Items.Add(menuItem);
            }
            else if (id.InvariantEquals(FormsConstants.Id))
            {

                // Actions for forms.
                AddCreateFormAction(menu);
                AddCreateFolderAction(menu);

            }
            else if (id.InvariantEquals(LayoutsConstants.Id))
            {

                // Actions for layouts.
                AddCreateLayoutAction(menu);
                AddCreateFolderAction(menu);

            }
            else
            {

                // Variables.
                var entityId = GuidHelper.GetGuid(id);
                var entity = TreeEntityPersistence.Retrieve(entityId);
                var ancestorId = entity.Path.First();


                // What type of entity does the node represent?
                if (entity is Form)
                {
                    AddDeleteFormAction(menu);
                }
                else if (entity is Layout)
                {
                    AddDeleteLayoutAction(menu);
                }
                else if (entity is Folder)
                {
                    AddCreateFolderAction(menu);
                    if (ancestorId == rootFormsId)
                    {
                        AddCreateFormAction(menu, entityId);
                    }
                    else if (ancestorId == rootLayoutId)
                    {
                        AddCreateLayoutAction(menu, entityId);
                    }
                }
                else
                {

                    //TODO: This is just for testing and should be removed eventually.
                    var path = "/App_Plugins/formulate/menu-actions/add.html";
                    var menuItem = new MenuItem()
                    {
                        Alias = "add",
                        Icon = "folder",
                        Name = "Add"
                    };
                    menuItem.LaunchDialogView(path, "Add");
                    menu.Items.Add(menuItem);

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
                var hasRootForms = TreeEntityPersistence
                    .RetrieveChildren(rootFormsId).Any();
                var formsNode = this.CreateTreeNode(FormsConstants.Id, id,
                    queryStrings, FormsConstants.Title, FormsConstants.Icon,
                    hasRootForms,
                    string.Format(formatUrl, "forms"));
                nodes.Add(formsNode);
                var hasRootLayouts = TreeEntityPersistence
                    .RetrieveChildren(rootLayoutsId).Any();
                var layoutsNode = this.CreateTreeNode(LayoutsConstants.Id,
                    id, queryStrings, LayoutsConstants.Title,
                    LayoutsConstants.Icon, hasRootLayouts,
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
                var entities = TreeEntityPersistence
                    .RetrieveChildren(rootLayoutsId);
                AddLayoutChildrenToTree(nodes, queryStrings, entities);

            }
            else if (id.InvariantEquals(FormsConstants.Id))
            {

                // Get root nodes under forms.
                var entities = TreeEntityPersistence
                    .RetrieveChildren(rootFormsId);
                AddFormChildrenToTree(nodes, queryStrings, entities);

            }
            else
            {

                // Variables.
                var entity = TreeEntityPersistence.Retrieve(entityId);
                var ancestorId = entity.Path.First();
                var children = TreeEntityPersistence
                    .RetrieveChildren(entityId);


                // Add children of folder.
                if (entity is Folder)
                {

                    // Which subtree?
                    if (ancestorId == rootFormsId)
                    {
                        AddFormChildrenToTree(nodes, queryStrings,
                            children);
                    }
                    else if (ancestorId == rootLayoutsId)
                    {
                        AddLayoutChildrenToTree(nodes, queryStrings,
                            children);
                    }

                }

            }
            return nodes;
        }


        private void AddCreateFormAction(MenuItemCollection menu,
            Guid? parentId = null)
        {

            // Configure "Create Form" action.
            var path = "/formulate/formulate/editForm/null";
            var menuItem = new MenuItem()
            {
                Alias = "createForm",
                Icon = "folder",
                Name = "Create Form"
            };
            if (parentId.HasValue)
            {
                path = path + "?under=" + GuidHelper.GetString(parentId.Value);
            }
            menuItem.NavigateToRoute(path);
            menu.Items.Add(menuItem);

        }


        private void AddCreateFolderAction(MenuItemCollection menu)
        {

            // Configure "Create Folder" button.
            var path = "/App_Plugins/formulate/menu-actions/createFolder.html";
            var menuItem = new MenuItem()
            {
                Alias = "createFolder",
                Icon = "folder",
                Name = "Create Folder"
            };
            menuItem.LaunchDialogView(path, "Create Folder");
            menu.Items.Add(menuItem);

        }


        private void AddCreateLayoutAction(MenuItemCollection menu,
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


        private void AddDeleteFormAction(MenuItemCollection menu)
        {

            // Configure "Delete Form" button.
            var path = "/App_Plugins/formulate/menu-actions/deleteForm.html";
            var menuItem = new MenuItem()
            {
                Alias = "deleteForm",
                Icon = "folder",
                Name = "Delete Form"
            };
            menuItem.LaunchDialogView(path, "Delete Form");
            menu.Items.Add(menuItem);

        }


        private void AddDeleteLayoutAction(MenuItemCollection menu)
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


        private void AddLayoutChildrenToTree(TreeNodeCollection nodes,
            FormDataCollection queryStrings,
            IEnumerable<IEntity> entities)
        {

            // Add folders/layouts to tree.
            foreach (var entity in entities)
            {
                if (entity is Folder)
                {
                    var folder = entity as Folder;
                    AddFolderToTree(nodes, queryStrings, folder);
                }
                else if (entity is Layout)
                {
                    var layout = entity as Layout;
                    AddLayoutToTree(nodes, queryStrings, layout);
                }
            }

        }


        private void AddLayoutToTree(TreeNodeCollection nodes,
            FormDataCollection queryStrings, Layout layout)
        {
            var formatUrl = "/formulate/formulate/editLayout/{0}";
            var layoutId = GuidHelper.GetString(layout.Id);
            var layoutRoute = string.Format(formatUrl, layoutId);
            var layoutName = layout.Name ?? "Unnamed";
            var layoutNode = this.CreateTreeNode(layoutId,
                LayoutsConstants.Id, queryStrings,
                layoutName, "icon-record", false, layoutRoute);
            nodes.Add(layoutNode);
        }

        private void AddFormChildrenToTree(TreeNodeCollection nodes,
            FormDataCollection queryStrings,
            IEnumerable<IEntity> entities)
        {

            // Add nodes for each entity.
            foreach (var entity in entities)
            {

                // Folder or form?
                if (entity is Folder)
                {
                    var folder = entity as Folder;
                    AddFolderToTree(nodes, queryStrings, folder);
                }
                else if (entity is Form)
                {
                    var form = entity as Form;
                    AddFormToTree(nodes, queryStrings, form);
                }

            }

        }


        private void AddFormToTree(TreeNodeCollection nodes,
            FormDataCollection queryStrings, Form form)
        {
            var formatUrl = "/formulate/formulate/editForm/{0}";
            var formId = GuidHelper.GetString(form.Id);
            var formRoute = string.Format(formatUrl, formId);
            var formName = form.Name ?? "Unnamed";
            var formNode = this.CreateTreeNode(formId,
                FormsConstants.Id, queryStrings,
                formName, "icon-record", false, formRoute);
            nodes.Add(formNode);
        }


        private void AddFolderToTree(TreeNodeCollection nodes,
            FormDataCollection queryStrings, Folder folder)
        {
            var folderFormat = "/formulate/formulate/folderInfo/{0}";
            var folderId = GuidHelper.GetString(folder.Id);
            var folderRoute = string.Format(folderFormat, folderId);
            var folderName = folder.Name ?? "Unnamed";
            var hasChildren = TreeEntityPersistence
                .RetrieveChildren(folder.Id).Any();
            var folderNode = this.CreateTreeNode(folderId,
                LayoutsConstants.Id, queryStrings, folderName,
                "icon-folder", hasChildren, folderRoute);
            nodes.Add(folderNode);
        }

    }

}