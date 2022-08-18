namespace Formulate.BackOffice.Trees
{
    using Formulate.BackOffice.Attributes;
    using Formulate.BackOffice.Persistence;
    using Formulate.Core.ConfiguredForms;
    using Formulate.Core.Persistence;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Umbraco.Cms.Core;
    using Umbraco.Cms.Core.Actions;
    using Umbraco.Cms.Core.Events;
    using Umbraco.Cms.Core.Services;
    using Umbraco.Cms.Core.Trees;
    using Umbraco.Cms.Web.BackOffice.Trees;
    using FormulateConstants = Constants;

    /// <summary>
    /// The Formulate forms tree controller.
    /// </summary>
    [Tree(FormulateSection.Constants.Alias, FormulateConstants.Trees.Forms, TreeTitle = "Forms", SortOrder = 0)]
    [FormulateBackOfficePluginController]
    public sealed class FormulateFormsTreeController : FormulateEntityTreeController
    {
        /// <inheritdoc />
        protected override TreeRootTypes TreeRootType => TreeRootTypes.Forms;

        /// <inheritdoc />
        protected override string RootNodeIcon => FormulateConstants.Icons.Roots.Forms;

        /// <inheritdoc />
        protected override string FolderNodeIcon => FormulateConstants.Icons.Folders.Forms;

        /// <inheritdoc />
        protected override string ItemNodeIcon => FormulateConstants.Icons.Entities.Form;

        /// <summary>
        /// Initializes a new instance of the <see cref="FormulateFormsTreeController"/> class.
        /// </summary>
        /// <inheritdoc />
        public FormulateFormsTreeController(ITreeEntityRepository treeEntityRepository, IMenuItemCollectionFactory menuItemCollectionFactory, ILocalizedTextService localizedTextService, UmbracoApiControllerTypeCollection umbracoApiControllerTypeCollection, IEventAggregator eventAggregator) : base(treeEntityRepository, menuItemCollectionFactory, localizedTextService, umbracoApiControllerTypeCollection, eventAggregator)
        {
        }

        /// <inheritdoc />
        protected override TreeNodeMetaData GetNodeMetaData(IPersistedEntity entity)
        {
            if (entity is PersistedConfiguredForm)
            {
                return new TreeNodeMetaData(FormulateConstants.Icons.Entities.ConfiguredForm);
            }

            return base.GetNodeMetaData(entity);
        }

        /// <inheritdoc />
        protected override ActionResult<MenuItemCollection> GetMenuForRoot(FormCollection queryStrings)
        {
            var menuItemCollection = MenuItemCollectionFactory.Create();

            //menuItemCollection.AddCreateFormMenuItem(default, LocalizedTextService);
            //menuItemCollection.AddCreateFolderMenuItem(LocalizedTextService);
            menuItemCollection.DefaultMenuAlias = ActionNew.ActionAlias;

            menuItemCollection.AddCreateDialogMenuItem(LocalizedTextService);
            menuItemCollection.AddRefreshMenuItem(LocalizedTextService);

            return menuItemCollection;
        }

        /// <inheritdoc />
        protected override ActionResult<MenuItemCollection> GetMenuForEntity(IPersistedEntity entity, FormCollection queryStrings)
        {
            var menuItemCollection = MenuItemCollectionFactory.Create();

            if (entity is PersistedConfiguredForm)
            {
                menuItemCollection.AddDeleteDialogMenuItem(LocalizedTextService);
            }
            else
            {
                menuItemCollection.DefaultMenuAlias = ActionNew.ActionAlias;

                menuItemCollection.AddCreateDialogMenuItem(LocalizedTextService);
                menuItemCollection.AddDeleteDialogMenuItem(LocalizedTextService);
                menuItemCollection.AddMoveDialogMenuItem(LocalizedTextService);

                menuItemCollection.AddRefreshMenuItem(LocalizedTextService);
            }

            return menuItemCollection;
        }
    }
}
