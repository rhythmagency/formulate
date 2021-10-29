using Formulate.BackOffice.Attributes;
using Formulate.BackOffice.Persistence;
using Formulate.Core.Folders;
using Formulate.Core.Layouts;
using Formulate.Core.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Trees;
using Umbraco.Cms.Web.BackOffice.Trees;

namespace Formulate.BackOffice.Trees
{
    /// <summary>
    /// The Formulate layouts tree controller.
    /// </summary>
    [Tree(FormulateSection.Constants.Alias, "layouts", TreeTitle = "Layouts", SortOrder = 1)]
    [FormulatePluginController]
    public sealed class FormulateLayoutsTreeController : FormulateTreeController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FormulateLayoutsTreeController"/> class.
        /// </summary>
        /// <inheritdoc />
        public FormulateLayoutsTreeController(ITreeEntityRepository treeEntityRepository, IMenuItemCollectionFactory menuItemCollectionFactory, ILocalizedTextService localizedTextService, UmbracoApiControllerTypeCollection umbracoApiControllerTypeCollection, IEventAggregator eventAggregator) : base(treeEntityRepository, menuItemCollectionFactory, localizedTextService, umbracoApiControllerTypeCollection, eventAggregator)
        {
        }

        /// <inheritdoc />
        protected override TreeRootTypes TreeRootType => TreeRootTypes.Layouts;

        /// <inheritdoc />
        protected override string RootNodeIcon => "icon-formulate-layouts";

        /// <inheritdoc />
        protected override string FolderNodeIcon => "icon-formulate-layout-group";

        /// <inheritdoc />
        protected override string ItemNodeIcon => "icon-formulate-layout";

        /// <inheritdoc />
        protected override string ItemNodeAction => "editLayout";
        
        /// <inheritdoc />
        protected override ActionResult<MenuItemCollection> GetMenuForRoot(FormCollection queryStrings)
        {
            var menuItemCollection = MenuItemCollectionFactory.Create();

            menuItemCollection.AddCreateDataValuesMenuItem(default, LocalizedTextService);
            menuItemCollection.AddCreateFolderMenuItem(LocalizedTextService);

            menuItemCollection.AddRefreshMenuItem(LocalizedTextService);

            return menuItemCollection;
        }

        /// <inheritdoc />
        protected override ActionResult<MenuItemCollection> GetMenuForEntity(IPersistedEntity entity, FormCollection queryStrings)
        {
            var menuItemCollection = MenuItemCollectionFactory.Create();

            if (entity is PersistedFolder folder)
            {
                menuItemCollection.AddCreateLayoutMenuItem(entity.Id, LocalizedTextService);
                menuItemCollection.AddCreateFolderMenuItem(LocalizedTextService);
                menuItemCollection.AddMoveFolderMenuItem(folder, LocalizedTextService);
                menuItemCollection.AddDeleteFolderMenuItem(LocalizedTextService);

                menuItemCollection.AddRefreshMenuItem(LocalizedTextService);
            }

            if (entity is PersistedLayout layout)
            {
                menuItemCollection.AddMoveLayoutMenuItem(layout, LocalizedTextService);
                menuItemCollection.AddDeleteLayoutMenuItem(LocalizedTextService);
            }

            return menuItemCollection;
        }
    }
}
