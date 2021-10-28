using Formulate.Core.Folders;
using Umbraco.Cms.Core.Models.Trees;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Trees;

namespace Formulate.BackOffice.Trees
{
    internal static partial class MenuItemCollectionExtensions
    {
        /// <summary>
        /// Add a create folder menu item to the menu.
        /// </summary>
        /// <param name="menuItemCollection">The current menu item collection.</param>
        /// <param name="localizedTextService">The localized text service.</param>
        public static void AddCreateFolderMenuItem(this MenuItemCollection menuItemCollection, ILocalizedTextService localizedTextService)
        {
            var path = "/App_Plugins/formulate/menu-actions/createFolder.html";
            var menuItem = new MenuItem()
            {
                Alias = "createFolder",
                Icon = "formulate-create-folder",
                Name = localizedTextService.GetMenuItemName("Create Folder")
            };

            menuItem.LaunchDialogView(path, "Create Folder");
            menuItemCollection.Items.Add(menuItem);
        }

        /// <summary>
        /// Add a delete folder menu item to the menu.
        /// </summary>
        /// <param name="menuItemCollection">The current menu item collection.</param>
        /// <param name="localizedTextService">The localized text service.</param>
        public static void AddDeleteFolderMenuItem(this MenuItemCollection menuItemCollection, ILocalizedTextService localizedTextService)
        {
            var path = "/App_Plugins/formulate/menu-actions/deleteFolder.html";
            var menuItem = new MenuItem()
            {
                Alias = "deleteFolder",
                Icon = "formulate-delete-folder",
                Name = localizedTextService.GetMenuItemName("Delete Folder")
            };
            menuItem.LaunchDialogView(path, "Delete Folder");
            menuItemCollection.Items.Add(menuItem);
        }
        
        /// <summary>
        /// Add a move folder menu item to the menu.
        /// </summary>
        /// <param name="menuItemCollection">The current menu item collection.</param>
        /// <param name="itemToMove">The item to move.</param>
        /// <param name="localizedTextService">The localized text service.</param>
        public static void AddMoveFolderMenuItem(this MenuItemCollection menuItemCollection, PersistedFolder itemToMove, ILocalizedTextService localizedTextService)
        {
            var path = "/App_Plugins/formulate/menu-actions/moveFolder.html";
            var menuItem = new MenuItem()
            {
                Alias = "moveFolder",
                Icon = "formulate-move",
                Name = localizedTextService.GetMenuItemName("Move") 
            };
            menuItem.LaunchDialogView(path, $"Move \"{itemToMove.Name}\" Folder");
            menuItemCollection.Items.Add(menuItem);
        }
    }
}
