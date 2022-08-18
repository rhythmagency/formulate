namespace Formulate.BackOffice.Trees
{
    using Formulate.BackOffice.Attributes;
    using Formulate.BackOffice.Persistence;
    using Formulate.Core.Folders;
    using Formulate.Core.Layouts;
    using Formulate.Core.Persistence;
    using Formulate.Core.Types;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Umbraco.Cms.Core;
    using Umbraco.Cms.Core.Events;
    using Umbraco.Cms.Core.Services;
    using Umbraco.Cms.Core.Trees;
    using Umbraco.Cms.Web.BackOffice.Trees;
    using FormulateConstants = Formulate.BackOffice.Constants;

    /// <summary>
    /// The Formulate layouts tree controller.
    /// </summary>
    [Tree(FormulateSection.Constants.Alias, FormulateConstants.Trees.Layouts, TreeTitle = "Layouts", SortOrder = 1)]
    [FormulateBackOfficePluginController]
    public sealed class FormulateLayoutsTreeController : FormulateEntityTreeController
    {
        private readonly LayoutDefinitionCollection _layoutDefinitions;

        /// <summary>
        /// Initializes a new instance of the <see cref="FormulateLayoutsTreeController"/> class.
        /// </summary>
        /// <inheritdoc />
        public FormulateLayoutsTreeController(LayoutDefinitionCollection layoutDefinitions, ITreeEntityRepository treeEntityRepository, IMenuItemCollectionFactory menuItemCollectionFactory, ILocalizedTextService localizedTextService, UmbracoApiControllerTypeCollection umbracoApiControllerTypeCollection, IEventAggregator eventAggregator) : base(treeEntityRepository, menuItemCollectionFactory, localizedTextService, umbracoApiControllerTypeCollection, eventAggregator)
        {
            _layoutDefinitions = layoutDefinitions;
        }

        /// <inheritdoc />
        protected override TreeRootTypes TreeRootType => TreeRootTypes.Layouts;

        /// <inheritdoc />
        protected override string RootNodeIcon => FormulateConstants.Icons.Roots.Layouts;

        /// <inheritdoc />
        protected override string FolderNodeIcon => FormulateConstants.Icons.Folders.Layouts;

        /// <inheritdoc />
        protected override string ItemNodeIcon => FormulateConstants.Icons.Entities.Layout;
        
        /// <inheritdoc />
        protected override ActionResult<MenuItemCollection> GetMenuForRoot(FormCollection queryStrings)
        {
            var menuItemCollection = MenuItemCollectionFactory.Create();

            menuItemCollection.AddCreateDialogMenuItem(LocalizedTextService);
            menuItemCollection.AddRefreshMenuItem(LocalizedTextService);

            return menuItemCollection;
        }

        /// <inheritdoc />
        protected override TreeNodeMetaData GetNodeMetaData(IPersistedEntity entity)
        {
            if (entity is PersistedLayout layoutEntity)
            {
                var definition = _layoutDefinitions.FirstOrDefault(layoutEntity.KindId);

                if (definition is not null)
                {
                    return new TreeNodeMetaData(ItemNodeIcon, definition.IsLegacy);
                }
            }

            return base.GetNodeMetaData(entity);
        }

        /// <inheritdoc />
        protected override ActionResult<MenuItemCollection> GetMenuForEntity(IPersistedEntity entity, FormCollection queryStrings)
        {
            var menuItemCollection = MenuItemCollectionFactory.Create();

            if (entity is PersistedFolder)
            {
                menuItemCollection.AddCreateDialogMenuItem(LocalizedTextService);
                menuItemCollection.AddDeleteDialogMenuItem(LocalizedTextService);
                menuItemCollection.AddMoveDialogMenuItem(LocalizedTextService);
                menuItemCollection.AddRefreshMenuItem(LocalizedTextService);
            }

            if (entity is PersistedLayout)
            {
                menuItemCollection.AddDeleteDialogMenuItem(LocalizedTextService);
                menuItemCollection.AddMoveDialogMenuItem(LocalizedTextService);
            }

            return menuItemCollection;
        }
    }
}
