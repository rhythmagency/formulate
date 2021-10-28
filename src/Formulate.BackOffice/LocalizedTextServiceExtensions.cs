using Umbraco.Cms.Core.Services;
using Umbraco.Extensions;

namespace Formulate.BackOffice
{
    /// <summary>
    /// Extension methods that augment classes that implement <see cref="ILocalizedTextService"/>.
    /// </summary>
    internal static class LocalizedTextServiceExtensions
    {
        /// <summary>
        /// Gets the name of the specified tree in the current language.
        /// </summary>
        /// <param name="localizedTextService">The current localized text service.</param>
        /// <param name="tree">
        /// The name of the tree.
        /// </param>
        /// <returns>
        /// The name of the tree in the current language.
        /// </returns>
        public static string GetTreeName(this ILocalizedTextService localizedTextService, string tree)
        {
            return localizedTextService.Localize("formulate-trees", tree);
        }

        /// <summary>
        /// Gets the name of the specified data value in the current language.
        /// </summary>
        /// <param name="localizedTextService">The current localized text service.</param>
        /// <param name="name">
        /// The name of the data value.
        /// </param>
        /// <returns>
        /// The name of the data value in the current language.
        /// </returns>
        public static string GetDataValueName(this ILocalizedTextService localizedTextService, string name)
        {
            return localizedTextService.Localize("formulate-data-value-names", name);
        }

        /// <summary>
        /// Gets the name of the specified layout in the current language.
        /// </summary>
        /// <param name="localizedTextService">The current localized text service.</param>
        /// <param name="name">
        /// The name of the layout.
        /// </param>
        /// <returns>
        /// The name of the layout in the current language.
        /// </returns>
        public static string GetLayoutName(this ILocalizedTextService localizedTextService, string name)
        {
            return localizedTextService.Localize("formulate-layout-names", name);
        }

        /// <summary>
        /// Gets the name of the specified menu item in the current language.
        /// </summary>
        /// <param name="localizedTextService">The current localized text service.</param>
        /// <param name="name">
        /// The name of the menu item.
        /// </param>
        /// <returns>
        /// The name of the menu item in the current language.
        /// </returns>
        public static string GetMenuItemName(this ILocalizedTextService localizedTextService, string name)
        {
            return localizedTextService.Localize("formulate-menu-item-names", name);
        }
    }
}
