using System;
using Formulate.Core.Layouts;
using Umbraco.Cms.Core.Models.Trees;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Trees;

namespace Formulate.BackOffice.Trees
{
    internal static partial class MenuItemCollectionExtensions
    {
        /// <summary>
        /// Add a create layout menu item to the menu.
        /// </summary>
        /// <param name="menuItemCollection">The current menu item collection.</param>
        /// <param name="parentId">An optional parent ID for the new node.</param>
        /// <param name="localizedTextService">The localized text service.</param>
        public static void AddCreateLayoutMenuItem(this MenuItemCollection menuItemCollection, Guid? parentId, ILocalizedTextService localizedTextService)
        {
            var path = "/App_Plugins/formulate/menu-actions/createLayout.html";
            var menuItem = new MenuItem()
            {
                Alias = "createLayout",
                Icon = "formulate-create",
                Name = localizedTextService.GetMenuItemName("Create Layout")
            };

            if (parentId.HasValue)
            {
                path = path + $"?under={parentId:N}";
            }
            menuItem.LaunchDialogView(path, "Create Layout");
            menuItemCollection.Items.Add(menuItem);
        }

        /// <summary>
        /// Add a delete layout menu item to the menu.
        /// </summary>
        /// <param name="menuItemCollection">The current menu item collection.</param>
        /// <param name="localizedTextService">The localized text service.</param>
        public static void AddDeleteLayoutMenuItem(this MenuItemCollection menuItemCollection, ILocalizedTextService localizedTextService)
        {
            var path = "/App_Plugins/formulate/menu-actions/deleteLayout.html";
            var menuItem = new MenuItem()
            {
                Alias = "deleteLayout",
                Icon = "formulate-delete",
                Name = localizedTextService.GetMenuItemName("Delete Layout")
            };
            menuItem.LaunchDialogView(path, "Delete Layout");
            menuItemCollection.Items.Add(menuItem);
        }
        
        /// <summary>
        /// Add a move layout menu item to the menu.
        /// </summary>
        /// <param name="menuItemCollection">The current menu item collection.</param>
        /// <param name="itemToMove">The item to move.</param>
        /// <param name="localizedTextService">The localized text service.</param>
        public static void AddMoveLayoutMenuItem(this MenuItemCollection menuItemCollection, PersistedLayout itemToMove, ILocalizedTextService localizedTextService)
        {
            var path = "/App_Plugins/formulate/menu-actions/moveLayout.html";
            var menuItem = new MenuItem()
            {
                Alias = "moveLayout",
                Icon = "formulate-move",
                Name =  localizedTextService.GetMenuItemName("Move")
            };

            menuItem.LaunchDialogView(path, $"Move \"{itemToMove.Name}\" Layout");
            menuItemCollection.Items.Add(menuItem);
        }
    }
}
