namespace formulate.app.Trees.Helpers
{

    // Namespaces.
    using Folders;
    using formulate.app.Helpers;
    using Persistence;
    using System.Linq;
    using System.Net.Http.Formatting;
    using Umbraco.Web.Models.Trees;
    using Umbraco.Web.Trees;


    internal class FolderHelper
    {

        private IEntityPersistence TreeEntityPersistence { get; set; }
        private TreeController Tree { get; set; }


        public FolderHelper(IEntityPersistence persistence,
            TreeController tree)
        {
            TreeEntityPersistence = persistence;
            Tree = tree;
        }


        public void AddFolderToTree(TreeNodeCollection nodes,
            FormDataCollection queryStrings, Folder folder, string icon)
        {
            var folderFormat = "/formulate/formulate/folderInfo/{0}";
            var folderId = GuidHelper.GetString(folder.Id);
            var folderRoute = string.Format(folderFormat, folderId);
            var folderName = folder.Name ?? "Unnamed";
            var hasChildren = TreeEntityPersistence
                .RetrieveChildren(folder.Id).Any();
            var parentId = folder.Path[folder.Path.Length - 2];
            var strParentId = GuidHelper.GetString(parentId);
            var folderNode = Tree.CreateTreeNode(folderId,
                strParentId, queryStrings, folderName,
                icon, hasChildren, folderRoute);
            nodes.Add(folderNode);
        }

        public void AddCreateFolderAction(MenuItemCollection menu)
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