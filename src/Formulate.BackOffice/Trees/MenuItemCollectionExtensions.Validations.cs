using System;
using Formulate.Core.Validations;
using Umbraco.Cms.Core.Models.Trees;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Trees;

namespace Formulate.BackOffice.Trees
{
    internal static partial class MenuItemCollectionExtensions
    {
        /// <summary>
        /// Add a create validation menu item to the menu.
        /// </summary>
        /// <param name="menuItemCollection">The current menu item collection.</param>
        /// <param name="parentId">An optional parent ID for the new node.</param>
        /// <param name="localizedTextService">The localized text service.</param>
        public static void AddCreateValidationMenuItem(this MenuItemCollection menuItemCollection, Guid? parentId, ILocalizedTextService localizedTextService)
        {
            var path = "/App_Plugins/formulate/menu-actions/createValidation.html";
            var menuItem = new MenuItem()
            {
                Alias = "createValidation",
                Icon = "formulate-create",
                Name = localizedTextService.GetMenuItemName("Create Validation")
            };

            if (parentId.HasValue)
            {
                path = path + $"?under={parentId:N}";
            }
            menuItem.LaunchDialogView(path, "Create Validation");
            menuItemCollection.Items.Add(menuItem);
        }

        /// <summary>
        /// Add a delete validation menu item to the menu.
        /// </summary>
        /// <param name="menuItemCollection">The current menu item collection.</param>
        /// <param name="localizedTextService">The localized text service.</param>
        public static void AddDeleteValidationMenuItem(this MenuItemCollection menuItemCollection, ILocalizedTextService localizedTextService)
        {
            var path = "/App_Plugins/formulate/menu-actions/deleteValidation.html";
            var menuItem = new MenuItem()
            {
                Alias = "deleteValidation",
                Icon = "formulate-delete",
                Name = localizedTextService.GetMenuItemName("Delete Validation")
            };
            menuItem.LaunchDialogView(path, "Delete Validation");
            menuItemCollection.Items.Add(menuItem);
        }

        /// <summary>
        /// Add a move validation menu item to the menu.
        /// </summary>
        /// <param name="menuItemCollection">The current menu item collection.</param>
        /// <param name="itemToMove">The item to move.</param>
        /// <param name="localizedTextService">The localized text service.</param>
        public static void AddMoveValidationMenuItem(this MenuItemCollection menuItemCollection, PersistedValidation itemToMove, ILocalizedTextService localizedTextService)
        {
            var path = "/App_Plugins/formulate/menu-actions/moveValidation.html";
            var menuItem = new MenuItem()
            {
                Alias = "moveValidation",
                Icon = "formulate-move",
                Name =  localizedTextService.GetMenuItemName("Move")
            };

            menuItem.LaunchDialogView(path, $"Move \"{itemToMove.Name}\" Validation");
            menuItemCollection.Items.Add(menuItem);
        }
    }
}
