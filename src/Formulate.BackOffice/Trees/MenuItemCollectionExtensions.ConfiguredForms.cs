using System;
using Umbraco.Cms.Core.Models.Trees;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Trees;

namespace Formulate.BackOffice.Trees
{
    internal static partial class MenuItemCollectionExtensions
    {
        /// <summary>
        /// Add a create configured form menu item to the menu.
        /// </summary>
        /// <param name="menuItemCollection">The current menu item collection.</param>
        /// <param name="parentId">The parent ID for the new node.</param>
        /// <param name="localizedTextService">The localized text service.</param>
        public static void AddCreateConfiguredFormMenuItem(this MenuItemCollection menuItemCollection, Guid parentId, ILocalizedTextService localizedTextService)
        {
            var path = $"/formulate/formulate/editConfiguredForm/null?under={parentId:N}";
            var menuItem = new MenuItem()
            {
                Alias = "createConfiguredForm",
                Icon = "formulate-create",
                Name = localizedTextService.GetMenuItemName("Create Configuration")
            };
            menuItem.NavigateToRoute(path);
            menuItemCollection.Items.Add(menuItem);
        }

        /// <summary>
        /// Add a delete configured form menu item to the menu.
        /// </summary>
        /// <param name="menuItemCollection">The current menu item collection.</param>
        /// <param name="localizedTextService">The localized text service.</param>
        public static void AddDeleteConfiguredFormMenuItem(this MenuItemCollection menuItemCollection, ILocalizedTextService localizedTextService)
        {
            var path = "/App_Plugins/formulate/menu-actions/deleteConfiguredForm.html";
            var menuItem = new MenuItem()
            {
                Alias = "deleteConfiguredForm",
                Icon = "formulate-delete",
                Name = localizedTextService.GetMenuItemName("Delete Configuration")
            };
            menuItem.LaunchDialogView(path, "Delete Configuration");
            menuItemCollection.Items.Add(menuItem);
        }
    }
}
