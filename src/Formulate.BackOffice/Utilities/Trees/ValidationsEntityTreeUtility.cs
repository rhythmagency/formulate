namespace Formulate.BackOffice.Utilities.Trees
{
    using Formulate.BackOffice.Persistence;
    using Formulate.BackOffice.Trees;
    using Formulate.Core.Persistence;
    using Formulate.Core.Types;
    using Formulate.Core.Validations;
    using Microsoft.AspNetCore.Http;
    using System.Collections.Generic;
    using Umbraco.Cms.Core.Actions;
    using Umbraco.Cms.Core.Services;
    using Umbraco.Cms.Core.Trees;

    internal sealed class ValidationsEntityTreeUtility : EntityTreeUtility, IValidationsEntityTreeUtility
    {
        private readonly ValidationDefinitionCollection _validationDefinitions;
        
        public ValidationsEntityTreeUtility(ValidationDefinitionCollection validationDefinitions, ILocalizedTextService localizedTextService, IMenuItemCollectionFactory menuItemCollectionFactory, ITreeEntityRepository treeEntityRepository) : base(localizedTextService, menuItemCollectionFactory, treeEntityRepository)
        {
            _validationDefinitions = validationDefinitions;
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

            if (entity is PersistedValidation)
            {
                menuItemCollection.AddDeleteDialogMenuItem(_localizedTextService);
                menuItemCollection.AddMoveDialogMenuItem(_localizedTextService);
            }

            return menuItemCollection;
        }

        protected override MenuItemCollection GetMenuForRoot(FormCollection queryStrings)
        {
            var menuItemCollection = _menuItemCollectionFactory.Create();

            menuItemCollection.DefaultMenuAlias = ActionNew.ActionAlias;

            menuItemCollection.AddCreateDialogMenuItem(_localizedTextService);
            menuItemCollection.AddRefreshMenuItem(_localizedTextService);

            return menuItemCollection;
        }

        protected override EntityTreeNodeMetaData GetNodeMetaData(IPersistedEntity entity)
        {
            if (entity.IsFolder())
            {
                return new EntityTreeNodeMetaData(Constants.Icons.Folders.Validations);
            }

            if (entity is PersistedValidation validationEntity)
            {
                var definition = _validationDefinitions.FirstOrDefault(validationEntity.KindId);

                if (definition is not null)
                {
                    return new EntityTreeNodeMetaData(Constants.Icons.Entities.Validation, definition.IsLegacy);
                }
            }

            return new EntityTreeNodeMetaData(Constants.Icons.Entities.Validation);
        }

        /// <inheritdoc />
        protected override IReadOnlyCollection<IPersistedEntity> GetRootEntities()
        {
            return _treeEntityRepository.GetRootItems(TreeRootTypes.Validations);
        }
    }
}
