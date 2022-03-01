using Formulate.BackOffice.Attributes;
using Formulate.BackOffice.Persistence;
using Formulate.Core.DataValues;
using Formulate.Core.Folders;
using Formulate.Core.Persistence;
using Formulate.Core.Types;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Actions;
using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Trees;
using Umbraco.Cms.Web.BackOffice.Trees;

namespace Formulate.BackOffice.Trees
{
    /// <summary>
    /// The Formulate data values tree controller.
    /// </summary>
    [Tree(FormulateSection.Constants.Alias, Constants.Alias, TreeTitle = "Data Values", SortOrder = 2)]
    [FormulateBackOfficePluginController]
    public sealed class FormulateDataValuesTreeController : FormulateTreeController
    {
        public static class Constants
        {
            public const string Alias = "datavalues";

            public const string RootNodeIcon = "icon-formulate-values";

            public const string FolderNodeIcon = "icon-formulate-value-group";

            public const string ItemNodeIcon = "icon-formulate-value";
        }

        /// <summary>
        /// The data values definitions.
        /// </summary>
        private readonly DataValuesDefinitionCollection _dataValuesDefinitions;

        /// <inheritdoc />
        protected override TreeRootTypes TreeRootType => TreeRootTypes.DataValues;

        /// <inheritdoc />
        protected override string RootNodeIcon => Constants.RootNodeIcon;

        /// <inheritdoc />
        protected override string FolderNodeIcon => Constants.FolderNodeIcon;

        /// <inheritdoc />
        protected override string ItemNodeIcon => Constants.ItemNodeIcon;

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
                var definition = _dataValuesDefinitions.FirstOrDefault(dataValuesEntity.KindId);

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
