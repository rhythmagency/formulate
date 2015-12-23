namespace formulate.app.Trees.Helpers
{

    // Namespaces.
    using Entities;
    using Folders;
    using Forms;
    using formulate.app.Helpers;
    using Persistence;
    using System;
    using System.Collections.Generic;
    using System.Net.Http.Formatting;
    using Umbraco.Web.Models.Trees;
    using Umbraco.Web.Trees;
    using FormsConstants = formulate.app.Constants.Trees.Forms;

    internal class FormHelper
    {

        private IEntityPersistence TreeEntityPersistence { get; set; }
        private TreeController Tree { get; set; }
        private FolderHelper Helper { get; set; }


        public FormHelper(IEntityPersistence persistence,
            TreeController tree, FolderHelper helper)
        {
            TreeEntityPersistence = persistence;
            Tree = tree;
            Helper = helper;
        }


        public void AddFormChildrenToTree(TreeNodeCollection nodes,
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
                    Helper.AddFolderToTree(nodes, queryStrings, folder,
                        FormsConstants.GroupIcon);
                }
                else if (entity is Form)
                {
                    var form = entity as Form;
                    AddFormToTree(nodes, queryStrings, form);
                }

            }

        }


        public void AddFormToTree(TreeNodeCollection nodes,
            FormDataCollection queryStrings, Form form)
        {
            var formatUrl = "/formulate/formulate/editForm/{0}";
            var formId = GuidHelper.GetString(form.Id);
            var formRoute = string.Format(formatUrl, formId);
            var formName = form.Name ?? "Unnamed";
            var parentId = form.Path[form.Path.Length - 2];
            var strParentId = GuidHelper.GetString(parentId);
            var formNode = Tree.CreateTreeNode(formId,
                strParentId, queryStrings,
                formName, FormsConstants.ItemIcon, false, formRoute);
            nodes.Add(formNode);
        }


        public void AddDeleteFormAction(MenuItemCollection menu)
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


        public void AddCreateFormAction(MenuItemCollection menu,
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

    }
}