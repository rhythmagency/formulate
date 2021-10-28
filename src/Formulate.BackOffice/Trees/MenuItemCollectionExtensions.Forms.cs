using System;
using Formulate.Core.Forms;
using Umbraco.Cms.Core.Models.Trees;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Trees;

namespace Formulate.BackOffice.Trees
{
    internal static partial class MenuItemCollectionExtensions
    {
        /// <summary>
        /// Add a create form menu item to the menu.
        /// </summary>
        /// <param name="menuItemCollection">The current menu item collection.</param>
        /// <param name="parentId">An optional parent ID for the new node.</param>
        /// <param name="localizedTextService">The localized text service.</param>
        public static void AddCreateFormMenuItem(this MenuItemCollection menuItemCollection, Guid? parentId, ILocalizedTextService localizedTextService)
        {
            var path = "/formulate/formulate/editForm/null";
            var menuItem = new MenuItem()
            {
                Alias = "createForm",
                Icon = "formulate-create",
                Name = localizedTextService.GetMenuItemName("Create Form")
            };
            if (parentId.HasValue)
            {
                path = path + $"?under={parentId:N}";
            }
            menuItem.NavigateToRoute(path);
            menuItemCollection.Items.Add(menuItem);
        }

        /// <summary>
        /// Add a delete form menu item to the menu.
        /// </summary>
        /// <param name="menuItemCollection">The current menu item collection.</param>
        /// <param name="localizedTextService">The localized text service.</param>
        public static void AddDeleteFormMenuItem(this MenuItemCollection menuItemCollection, ILocalizedTextService localizedTextService)
        {
            var path = "/App_Plugins/formulate/menu-actions/deleteForm.html";
            var menuItem = new MenuItem()
            {
                Alias = "deleteForm",
                Icon = "formulate-delete",
                Name = localizedTextService.GetMenuItemName("Delete Form")
            };
            menuItem.LaunchDialogView(path, "Delete Form");
            menuItemCollection.Items.Add(menuItem);
        }

        /// <summary>
        /// Add a move form menu item to the menu.
        /// </summary>
        /// <param name="menuItemCollection">The current menu item collection.</param>
        /// <param name="itemToMove">The item to move.</param>
        /// <param name="localizedTextService">The localized text service.</param>
        public static void AddMoveFormMenuItem(this MenuItemCollection menuItemCollection, PersistedForm itemToMove, ILocalizedTextService localizedTextService)
        {
            var path = "/App_Plugins/formulate/menu-actions/moveForm.html";
            var menuItem = new MenuItem()
            {
                Alias = "moveForm",
                Icon = "formulate-move",
                Name = localizedTextService.GetMenuItemName("Move")
            };
            var titleFormat = @"Move ""{0}"" Form";
            var title = string.Format(titleFormat, itemToMove.Name);
            menuItem.LaunchDialogView(path, title);
            menuItem.LaunchDialogView(path, $"Move \"{itemToMove.Name}\" Form");
            menuItemCollection.Items.Add(menuItem);
        }
    }
}
