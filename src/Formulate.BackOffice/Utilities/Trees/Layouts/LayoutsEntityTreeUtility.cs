namespace Formulate.BackOffice.Utilities.Trees.Layouts
{
    using Formulate.BackOffice.Persistence;
    using Formulate.BackOffice.Trees;
    using Formulate.Core.Layouts;
    using Formulate.Core.Persistence;
    using Formulate.Core.Types;
    using Microsoft.AspNetCore.Http;
    using System.Collections.Generic;
    using Umbraco.Cms.Core.Actions;
    using Umbraco.Cms.Core.Services;
    using Umbraco.Cms.Core.Trees;

    internal sealed class LayoutsEntityTreeUtility : EntityTreeUtility, ILayoutsEntityTreeUtility
    {
        private readonly LayoutDefinitionCollection _layoutDefinitions;

        public LayoutsEntityTreeUtility(LayoutDefinitionCollection layoutDefinitions, ILocalizedTextService localizedTextService, IMenuItemCollectionFactory menuItemCollectionFactory, ITreeEntityRepository treeEntityRepository) : base(localizedTextService, menuItemCollectionFactory, treeEntityRepository)
        {
            _layoutDefinitions = layoutDefinitions;
        }

        protected override MenuItemCollection GetMenuForEntity(IPersistedEntity entity, FormCollection queryStrings)
        {
            var menuItemCollection = _menuItemCollectionFactory.Create();

            if (entity.IsFolder())
            {
                menuItemCollection.AddCreateDialogMenuItem(_localizedTextService);
                menuItemCollection.AddDeleteDialogMenuItem(_localizedTextService);
                menuItemCollection.AddMoveDialogMenuItem(_localizedTextService);
                menuItemCollection.AddRefreshMenuItem(_localizedTextService);
            }

            if (entity is PersistedLayout)
            {
                menuItemCollection.AddDeleteDialogMenuItem(_localizedTextService);
                menuItemCollection.AddMoveDialogMenuItem(_localizedTextService);
            }

            return menuItemCollection;
        }

        protected override EntityTreeNodeMetaData GetNodeMetaData(IPersistedEntity entity)
        {
            if (entity.IsFolder())
            {
                return new EntityTreeNodeMetaData(Constants.Icons.Folders.Layouts);
            }

            if (entity is PersistedLayout layoutEntity)
            {
                var definition = _layoutDefinitions.FirstOrDefault(layoutEntity.KindId);

                if (definition is not null)
                {
                    return new EntityTreeNodeMetaData(Constants.Icons.Entities.Layout, definition.IsLegacy);
                }
            }

            return new EntityTreeNodeMetaData(Constants.Icons.Entities.Layout);
        }

        /// <inheritdoc />
        protected override IReadOnlyCollection<IPersistedEntity> GetRootEntities()
        {
            return _treeEntityRepository.GetRootItems(TreeRootTypes.Layouts);
        }
    }
}
