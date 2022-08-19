namespace Formulate.BackOffice.Utilities.Trees
{
    using Formulate.BackOffice.Persistence;
    using Formulate.BackOffice.Trees;
    using Formulate.Core.Folders;
    using Formulate.Core.Persistence;
    using Microsoft.AspNetCore.Http;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Umbraco.Cms.Core.Actions;
    using Umbraco.Cms.Core.Services;
    using Umbraco.Cms.Core.Trees;
    using Umbraco.Extensions;

    /// <summary>
    /// An abstract implementation of <see cref="IEntityTreeUtility"/> which provide some common functionality for all inheriting class.
    /// </summary>
    internal abstract class EntityTreeUtility : IEntityTreeUtility
    {
        protected readonly ILocalizedTextService _localizedTextService;
        protected readonly IMenuItemCollectionFactory _menuItemCollectionFactory;
        protected readonly ITreeEntityRepository _treeEntityRepository;

        public EntityTreeUtility(ILocalizedTextService localizedTextService, IMenuItemCollectionFactory menuItemCollectionFactory, ITreeEntityRepository treeEntityRepository)
        {
            _localizedTextService = localizedTextService;
            _menuItemCollectionFactory = menuItemCollectionFactory;
            _treeEntityRepository = treeEntityRepository;
        }

        /// <inheritdoc />
        public MenuItemCollection GetMenuItems(GetMenuItemsInput input)
        {
            var queryStrings = input.QueryStrings;

            if (IsTreeRootId(input.Id))
            {
                return GetMenuForRoot(queryStrings);
            }

            if (Guid.TryParse(input.Id, out var entityId))
            {
                var entity = _treeEntityRepository.Get(entityId);
                if (entity is not null)
                {
                    return GetMenuForEntity(entity, queryStrings);
                }
            }

            return _menuItemCollectionFactory.Create();
        }
        
        /// <inheritdoc />
        public IReadOnlyCollection<EntityTreeNode> GetTreeNodes(GetTreeNodesInput input)
        {
            var id = input.Id;
            var queryStrings = input.QueryStrings;
            var isFolderOnly = string.IsNullOrEmpty(queryStrings["foldersonly"]) == false && queryStrings["foldersonly"] == "1";
            var entities = GetEntities(id);
            var filteredEntities = isFolderOnly ? entities.OfType<PersistedFolder>().ToArray() : entities;
            var nodes = new List<EntityTreeNode>();
            Func<IPersistedEntity, bool>? hasChildrenFilter = isFolderOnly ? (entity) => entity.IsFolder() : default;

            foreach (var entity in filteredEntities)
            {
                var hasChildren = _treeEntityRepository.HasChildren(entity.Id, hasChildrenFilter);
                var metaData = GetNodeMetaData(entity);

                nodes.Add(new EntityTreeNode(entity, hasChildren, metaData.Icon, metaData.IsLegacy));             
            }

            return nodes;
        }

        protected virtual MenuItemCollection GetMenuForRoot(FormCollection queryStrings)
        {
            var menuItemCollection = _menuItemCollectionFactory.Create();

            menuItemCollection.DefaultMenuAlias = ActionNew.ActionAlias;

            menuItemCollection.AddCreateDialogMenuItem(_localizedTextService); ;
            menuItemCollection.AddRefreshMenuItem(_localizedTextService);

            return menuItemCollection;
        }

        protected abstract TreeNodeMetaData GetNodeMetaData(IPersistedEntity entity);

        protected abstract IReadOnlyCollection<IPersistedEntity> GetRootEntities();

        protected abstract MenuItemCollection GetMenuForEntity(IPersistedEntity entity, FormCollection queryStrings);

        /// <summary>
        /// Gets the entities used to populate the tree.
        /// </summary>
        /// <param name="id">The current id.</param>
        /// <returns>A read only collection of <see cref="IPersistedEntity"/>.</returns>
        private IReadOnlyCollection<IPersistedEntity> GetEntities(string id)
        {
            if (IsTreeRootId(id))
            {
                return GetRootEntities();
            }

            if (Guid.TryParse(id, out var parentId))
            {
                return _treeEntityRepository.GetChildren(parentId);
            }

            return Array.Empty<IPersistedEntity>();
        }

        private static bool IsTreeRootId(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return false;
            }

            return id.Equals(Umbraco.Cms.Core.Constants.System.Root.ToInvariantString());
        }
    }
}
