namespace Formulate.BackOffice.Trees
{
    using Formulate.BackOffice.Attributes;
    using Formulate.BackOffice.Persistence;
    using Formulate.Core.DataValues;
    using Formulate.Core.Folders;
    using Formulate.Core.Persistence;
    using Formulate.Core.Types;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Umbraco.Cms.Core;
    using Umbraco.Cms.Core.Actions;
    using Umbraco.Cms.Core.Events;
    using Umbraco.Cms.Core.Services;
    using Umbraco.Cms.Core.Trees;
    using Umbraco.Cms.Web.BackOffice.Trees;
    using FormulateConstants = Formulate.BackOffice.Constants;

    /// <summary>
    /// The Formulate data values tree controller.
    /// </summary>
    [Tree(FormulateSection.Constants.Alias, FormulateConstants.Trees.DataValues, TreeTitle = "Data Values", SortOrder = 2)]
    [FormulateBackOfficePluginController]
    public sealed class FormulateDataValuesTreeController : FormulateEntityTreeController
    {
        /// <summary>
        /// The data values definitions.
        /// </summary>
        private readonly DataValuesDefinitionCollection _dataValuesDefinitions;

        /// <inheritdoc />
        protected override TreeRootTypes TreeRootType => TreeRootTypes.DataValues;

        /// <inheritdoc />
        protected override string RootNodeIcon => FormulateConstants.Icons.Roots.DataValues;

        /// <inheritdoc />
        protected override string FolderNodeIcon => FormulateConstants.Icons.Folders.DataValues;

        /// <inheritdoc />
        protected override string ItemNodeIcon => FormulateConstants.Icons.Entities.DataValues;

        /// <summary>
        /// Initializes a new instance of the <see cref="FormulateDataValuesTreeController"/> class.
        /// </summary>
        /// <param name="dataValuesDefinitions">The data values definitions.</param>
        /// <param name="treeEntityRepository">The tree entity repository.</param>
        /// <param name="menuItemCollectionFactory">The menu item collection factory.</param>
        /// <param name="localizedTextService">The localized text service.</param>
        /// <param name="umbracoApiControllerTypeCollection">The umbraco api controller type collection.</param>
        /// <param name="eventAggregator">The event aggregator.</param>
        public FormulateDataValuesTreeController(DataValuesDefinitionCollection dataValuesDefinitions, ITreeEntityRepository treeEntityRepository, IMenuItemCollectionFactory menuItemCollectionFactory, ILocalizedTextService localizedTextService, UmbracoApiControllerTypeCollection umbracoApiControllerTypeCollection, IEventAggregator eventAggregator) : base(treeEntityRepository, menuItemCollectionFactory, localizedTextService, umbracoApiControllerTypeCollection, eventAggregator)
        {
            _dataValuesDefinitions = dataValuesDefinitions;
        }

        /// <inheritdoc />
        protected override TreeNodeMetaData GetNodeMetaData(IPersistedEntity entity)
        {
            if (entity is PersistedDataValues dataValuesEntity)
            {
                var definition = _dataValuesDefinitions.FirstOrDefault(dataValuesEntity.KindId);

                if (definition is not null)
                {
                    var icon = definition.Icon;

                    if (string.IsNullOrWhiteSpace(icon) == false)
                    {
                        return new TreeNodeMetaData(icon, definition.IsLegacy);
                    }
                }
            }

            return base.GetNodeMetaData(entity);
        }

        /// <inheritdoc />
        protected override ActionResult<MenuItemCollection> GetMenuForRoot(FormCollection queryStrings)
        {
            var menuItemCollection = MenuItemCollectionFactory.Create();

            menuItemCollection.DefaultMenuAlias = ActionNew.ActionAlias;

            menuItemCollection.AddCreateDialogMenuItem(LocalizedTextService);
            menuItemCollection.AddRefreshMenuItem(LocalizedTextService);

            return menuItemCollection;
        }

        /// <inheritdoc />
        protected override ActionResult<MenuItemCollection> GetMenuForEntity(IPersistedEntity entity, FormCollection queryStrings)
        {
            var menuItemCollection = MenuItemCollectionFactory.Create();

            if (entity is PersistedFolder)
            {
                menuItemCollection.DefaultMenuAlias = ActionNew.ActionAlias;
                menuItemCollection.AddCreateDialogMenuItem(LocalizedTextService);
                menuItemCollection.AddDeleteDialogMenuItem(LocalizedTextService);
                menuItemCollection.AddMoveDialogMenuItem(LocalizedTextService);
                menuItemCollection.AddRefreshMenuItem(LocalizedTextService);
            }

            if (entity is PersistedDataValues)
            {
                menuItemCollection.AddDeleteDialogMenuItem(LocalizedTextService);
                menuItemCollection.AddMoveDialogMenuItem(LocalizedTextService);
            }

            return menuItemCollection;
        }
    }
}
