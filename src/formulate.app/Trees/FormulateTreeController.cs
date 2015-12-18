namespace formulate.app.Trees
{

    // Namespaces.
    using Forms;
    using Layouts;
    using Helpers;
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

        private ILayoutPersistence TreeLayoutPersistence { get; set; }
        private IFormPersistence TreeFormPersistence { get; set; }


        public FormulateTreeController()
        {
            TreeLayoutPersistence = LayoutPersistence.Current.Manager;
            TreeFormPersistence = FormPersistence.Current.Manager;
        }

        protected override MenuItemCollection GetMenuForNode(string id,
            FormDataCollection queryStrings)
        {
            var menu = new MenuItemCollection();
            var rootId = CoreConstants.System.Root.ToInvariantString();
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

                AddCreateFormAction(menu);
                AddCreateFolderAction(menu);

            }
            else if (id.InvariantEquals(LayoutsConstants.Id))
            {

                // Configure "Create Layout" button.
                var path = "/App_Plugins/formulate/menu-actions/createLayout.html";
                var menuItem = new MenuItem()
                {
                    Alias = "createLayout",
                    Icon = "folder",
                    Name = "Create Layout"
                };
                menuItem.LaunchDialogView(path, "Create Layout");
                menu.Items.Add(menuItem);

            }
            else
            {

                // Variables.
                var entityId = GuidHelper.GetGuid(id);
                var entity = GetEntity(entityId);


                // What type of entity does the node represent?
                if (entity is Form)
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
                else if (entity is Layout)
                {
                    //TODO: ...
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

        protected override TreeNodeCollection GetTreeNodes(string id, FormDataCollection queryStrings)
        {
            var nodes = new TreeNodeCollection();
            var rootId = CoreConstants.System.Root.ToInvariantString();
            if (id.InvariantEquals(rootId))
            {

                // Get root nodes.
                var formatUrl = "/formulate/formulate/{0}/info";
                var hasRootForms = TreeFormPersistence.RetrieveChildren(null).Any();
                var formsNode = this.CreateTreeNode(FormsConstants.Id, id,
                    queryStrings, FormsConstants.Title, FormsConstants.Icon, hasRootForms,
                    string.Format(formatUrl, "forms"));
                nodes.Add(formsNode);
                var hasRootLayouts = TreeLayoutPersistence.RetrieveChildren(null).Any();
                var layoutsNode = this.CreateTreeNode(LayoutsConstants.Id, id,
                    queryStrings, LayoutsConstants.Title, LayoutsConstants.Icon, hasRootLayouts,
                    string.Format(formatUrl, "layouts"));
                nodes.Add(layoutsNode);
                var dataSourcesNode = this.CreateTreeNode(DataSourcesConstants.Id, id,
                    queryStrings, DataSourcesConstants.Title, DataSourcesConstants.Icon, false,
                    string.Format(formatUrl, "dataSources"));
                nodes.Add(dataSourcesNode);
                var dataValuesNode = this.CreateTreeNode(DataValuesConstants.Id, id,
                    queryStrings, DataValuesConstants.Title, DataValuesConstants.Icon, false,
                    string.Format(formatUrl, "dataValues"));
                nodes.Add(dataValuesNode);
                var validationsNode = this.CreateTreeNode(ValidationsConstants.Id, id,
                    queryStrings, ValidationsConstants.Title, ValidationsConstants.Icon, false,
                    string.Format(formatUrl, "validationLibrary"));
                nodes.Add(validationsNode);

            }
            else if (id.InvariantEquals(LayoutsConstants.Id))
            {

                // Get layout nodes.
                var formatUrl = "/formulate/formulate/editLayout/{0}";
                var rootLayouts = TreeLayoutPersistence.RetrieveChildren(null);
                foreach (var layout in rootLayouts)
                {
                    var layoutId = GuidHelper.GetString(layout.Id);
                    var layoutRoute = string.Format(formatUrl, layoutId);
                    var layoutName = layout.Name ?? "Unnamed";
                    var layoutNode = this.CreateTreeNode(layoutId, LayoutsConstants.Id, queryStrings,
                        layoutName, "icon-folder", false, layoutRoute);
                    nodes.Add(layoutNode);
                }

            }
            else if (id.InvariantEquals(FormsConstants.Id))
            {

                // Get form nodes.
                var formatUrl = "/formulate/formulate/editForm/{0}";
                var rootForms = TreeFormPersistence.RetrieveChildren(null);
                foreach (var form in rootForms)
                {
                    var formId = GuidHelper.GetString(form.Id);
                    var formRoute = string.Format(formatUrl, formId);
                    var formName = form.Name ?? "Unnamed";
                    var formNode = this.CreateTreeNode(formId, LayoutsConstants.Id, queryStrings,
                        formName, "icon-folder", false, formRoute);
                    nodes.Add(formNode);
                }

            }
            return nodes;
        }

        private object GetEntity(Guid id)
        {
            var form = TreeFormPersistence.Retrieve(id);
            var layout = TreeLayoutPersistence.Retrieve(id);
            return form as object ?? layout;
        }


        private void AddCreateFormAction(MenuItemCollection menu)
        {

            // Configure "Create Form" action.
            var path = "/formulate/formulate/editForm/null";
            var menuItem = new MenuItem()
            {
                Alias = "createForm",
                Icon = "folder",
                Name = "Create Form"
            };
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

    }

}