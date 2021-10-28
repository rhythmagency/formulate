using Formulate.BackOffice.Attributes;
using Formulate.BackOffice.Persistence;
using Formulate.Core.DataValues;
using Formulate.Core.Folders;
using Formulate.Core.Persistence;
using Formulate.Core.Types;
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
    /// The Formulate data values tree controller.
    /// </summary>
    [Tree(FormulateSection.Constants.Alias, "datavalues", TreeTitle = "Data Values", SortOrder = 2)]
    [FormulatePluginController]
    public sealed class FormulateDataValuesTreeController : FormulateTreeController
    {
        /// <summary>
        /// The data values definitions.
        /// </summary>
        private readonly DataValuesDefinitionCollection _dataValuesDefinitions;

        /// <inheritdoc />
        protected override FormulateEntityTypes EntityType => FormulateEntityTypes.DataValues;

        /// <inheritdoc />
        protected override string RootNodeIcon => "icon-formulate-values";

        /// <inheritdoc />
        protected override string FolderNodeIcon => "icon-formulate-value-group";

        /// <inheritdoc />
        protected override string ItemNodeIcon => "icon-formulate-value";

        /// <inheritdoc />
        protected override string ItemNodeAction => "editDataValue";

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
        protected override string GetNodeIcon(IPersistedEntity entity)
        {
            if (entity is PersistedDataValues dataValuesEntity)
            {
                var definition = _dataValuesDefinitions.FirstOrDefault(dataValuesEntity.DefinitionId);

                if (definition is not null)
                {
                    var icon = definition.Icon;

                    if (string.IsNullOrWhiteSpace(icon) == false)
                    {
                        return icon;
                    }
                }
            }

            return base.GetNodeIcon(entity);
        }

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
                menuItemCollection.AddCreateDataValuesMenuItem(entity.Id, LocalizedTextService);
                menuItemCollection.AddCreateFolderMenuItem(LocalizedTextService);
                menuItemCollection.AddMoveFolderMenuItem(folder, LocalizedTextService);
                menuItemCollection.AddDeleteFolderMenuItem(LocalizedTextService);

                menuItemCollection.AddRefreshMenuItem(LocalizedTextService);
            }

            if (entity is PersistedDataValues dataValues)
            {
                menuItemCollection.AddMoveDataValuesMenuItem(dataValues, LocalizedTextService);
                menuItemCollection.AddDeleteDataValuesMenuItem(LocalizedTextService);
            }

            return menuItemCollection;
        }
    }
}
