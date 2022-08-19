namespace Formulate.BackOffice.Utilities.Trees.DataValues
{
    using Formulate.BackOffice.Persistence;
    using Formulate.BackOffice.Trees;
    using Formulate.Core.DataValues;
    using Formulate.Core.Persistence;
    using Formulate.Core.Types;
    using Microsoft.AspNetCore.Http;
    using System.Collections.Generic;
    using Umbraco.Cms.Core.Actions;
    using Umbraco.Cms.Core.Services;
    using Umbraco.Cms.Core.Trees;

    internal sealed class DataValuesEntityTreeUtility : EntityTreeUtility, IDataValuesEntityTreeUtility
    {
        private readonly DataValuesDefinitionCollection _dataValuesDefinitions;

        public DataValuesEntityTreeUtility(DataValuesDefinitionCollection dataValuesDefinitions, ILocalizedTextService localizedTextService, IMenuItemCollectionFactory menuItemCollectionFactory, ITreeEntityRepository treeEntityRepository) : base(localizedTextService, menuItemCollectionFactory, treeEntityRepository)
        {
            _dataValuesDefinitions = dataValuesDefinitions;
        }

        protected override MenuItemCollection GetMenuForEntity(IPersistedEntity entity, FormCollection queryStrings)
        {
            var menuItemCollection = _menuItemCollectionFactory.Create();

            if (entity.IsFolder())
            {
                menuItemCollection.DefaultMenuAlias = ActionNew.ActionAlias;
                menuItemCollection.AddCreateDialogMenuItem(_localizedTextService);
                menuItemCollection.AddDeleteDialogMenuItem(_localizedTextService);
                menuItemCollection.AddMoveDialogMenuItem(_localizedTextService);
                menuItemCollection.AddRefreshMenuItem(_localizedTextService);
            }

            if (entity is PersistedDataValues)
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
                return new EntityTreeNodeMetaData(Constants.Icons.Folders.DataValues);
            }

            if (entity is PersistedDataValues dataValuesEntity)
            {
                var definition = _dataValuesDefinitions.FirstOrDefault(dataValuesEntity.KindId);

                if (definition is not null)
                {
                    var icon = definition.Icon;

                    if (string.IsNullOrWhiteSpace(icon) == false)
                    {
                        return new EntityTreeNodeMetaData(icon, definition.IsLegacy);
                    }
                }
            }

            return new EntityTreeNodeMetaData(Constants.Icons.Entities.DataValues);
        }

        /// <inheritdoc />
        protected override IReadOnlyCollection<IPersistedEntity> GetRootEntities()
        {
            return _treeEntityRepository.GetRootItems(TreeRootTypes.DataValues);
        }
    }
}
