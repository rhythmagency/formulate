namespace formulate.app.Trees
{

    // Namespaces.
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

        protected override MenuItemCollection GetMenuForNode(string id, FormDataCollection queryStrings)
        {
            var menu = new MenuItemCollection();
            var rootId = CoreConstants.System.Root.ToInvariantString();
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
            else if (id.InvariantEquals(FormsConstants.Id))
            {
                var path = "/formulate/formulate/editForm/null?create";
                var menuItem = new MenuItem()
                {
                    Alias = "add",
                    Icon = "icon-folder",
                    Name = "Create Form"
                };
                menuItem.NavigateToRoute(path);
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
            var rootId = CoreConstants.System.Root.ToInvariantString();
            if (id.InvariantEquals(rootId))
            {
                var formatUrl = "/formulate/formulate/{0}/info";
                var formsNode = this.CreateTreeNode(FormsConstants.Id, id,
                    queryStrings, FormsConstants.Title, FormsConstants.Icon, false,
                    string.Format(formatUrl, "forms"));
                nodes.Add(formsNode);
                var layoutsNode = this.CreateTreeNode(LayoutsConstants.Id, id,
                    queryStrings, LayoutsConstants.Title, LayoutsConstants.Icon, false,
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
            return nodes;
        }

    }

}