using System;
using Formulate.Core.DataValues;
using Umbraco.Cms.Core.Models.Trees;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Trees;

namespace Formulate.BackOffice.Trees
{
    internal static partial class MenuItemCollectionExtensions
    {
        /// <summary>
        /// Add a create data values menu item to the menu.
        /// </summary>
        /// <param name="menuItemCollection">The current menu item collection.</param>
        /// <param name="parentId">An optional parent ID for the new node.</param>
        /// <param name="localizedTextService">The localized text service.</param>
        public static void AddCreateDataValuesMenuItem(this MenuItemCollection menuItemCollection, Guid? parentId, ILocalizedTextService localizedTextService)
        {
            var path = "/App_Plugins/formulate/menu-actions/createDataValues.html";
            var menuItem = new MenuItem()
            {
                Alias = "createDataValues",
                Icon = "formulate-create",
                Name = localizedTextService.GetMenuItemName("Create Data Values")
            };

            if (parentId.HasValue)
            {
                path = path + $"?under={parentId:N}";
            }
            menuItem.LaunchDialogView(path, "Create Data Values");
            menuItemCollection.Items.Add(menuItem);
        }

        /// <summary>
        /// Add a delete data values menu item to the menu.
        /// </summary>
        /// <param name="menuItemCollection">The current menu item collection.</param>
        /// <param name="localizedTextService">The localized text service.</param>
        public static void AddDeleteDataValuesMenuItem(this MenuItemCollection menuItemCollection, ILocalizedTextService localizedTextService)
        {
            var path = "/App_Plugins/formulate/menu-actions/deleteDataValues.html";
            var menuItem = new MenuItem()
            {
                Alias = "deleteDataValues",
                Icon = "formulate-delete",
                Name = localizedTextService.GetMenuItemName("Delete Data Values")
            };
            menuItem.LaunchDialogView(path, "Delete Data Values");
            menuItemCollection.Items.Add(menuItem);
        }

        /// <summary>
        /// Add a move data values menu item to the menu.
        /// </summary>
        /// <param name="menuItemCollection">The current menu item collection.</param>
        /// <param name="itemToMove">The item to move.</param>
        /// <param name="localizedTextService">The localized text service.</param>
        public static void AddMoveDataValuesMenuItem(this MenuItemCollection menuItemCollection, PersistedDataValues itemToMove, ILocalizedTextService localizedTextService)
        {
            var path = "/App_Plugins/formulate/menu-actions/moveDataValues.html";
            var menuItem = new MenuItem()
            {
                Alias = "moveDataValues",
                Icon = "formulate-move",
                Name =  localizedTextService.GetMenuItemName("Move")
            };

            menuItem.LaunchDialogView(path, $"Move \"{itemToMove.Name}\" Data Values");
            menuItemCollection.Items.Add(menuItem);
        }
    }
}
