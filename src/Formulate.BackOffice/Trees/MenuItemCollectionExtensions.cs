using System.Runtime.InteropServices;
using Umbraco.Cms.Core.Actions;
using Umbraco.Cms.Core.Models.Trees;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Trees;

namespace Formulate.BackOffice.Trees
{
    /// <summary>
    /// Extension methods that augments the <see cref="MenuItemCollection"/> class.
    /// </summary>
    internal static partial class MenuItemCollectionExtensions
    {
        /// <summary>
        /// Add a create child entity dialog menu item to the menu.
        /// </summary>
        /// <param name="menuItemCollection">The current menu item collection.</param>
        /// <param name="localizedTextService">The localized text service.</param>
        /// <param name="separatorBefore">Determines if we add a separator before this menu item.</param>
        public static void AddCreateDialogMenuItem(this MenuItemCollection menuItemCollection,
            ILocalizedTextService localizedTextService, bool separatorBefore = false)
        {
            menuItemCollection.Items.Add<ActionNew>(localizedTextService, opensDialog: true);
        }

        /// <summary>
        /// Add a refresh menu item to the menu.
        /// </summary>
        /// <param name="menuItemCollection">The current menu item collection.</param>
        /// <param name="localizedTextService">The localized text service.</param>
        /// <param name="separatorBefore">Determines if we add a separator before this menu item.</param>
        public static void AddRefreshMenuItem(this MenuItemCollection menuItemCollection,
            ILocalizedTextService localizedTextService, bool separatorBefore = true)
        {
            menuItemCollection.Items.Add(new RefreshNode(localizedTextService, separatorBefore));
        }
    }
}
