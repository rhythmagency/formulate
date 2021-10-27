using System;
using System.Collections.Generic;
using Formulate.Core.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Trees;
using Umbraco.Cms.Web.BackOffice.Trees;
using Umbraco.Extensions;
using ITreeEntityPersistence = Formulate.BackOffice.Persistence.ITreeEntityPersistence;

namespace Formulate.BackOffice.Trees
{
    public abstract class FormulateTreeController : TreeController
    {
        private readonly ITreeEntityPersistence _treeEntityPersistence;

        private readonly IMenuItemCollectionFactory _menuItemCollectionFactory;

        protected FormulateTreeController(ITreeEntityPersistence treeEntityPersistence,
            IMenuItemCollectionFactory menuItemCollectionFactory, ILocalizedTextService localizedTextService,
            UmbracoApiControllerTypeCollection umbracoApiControllerTypeCollection, IEventAggregator eventAggregator) :
            base(localizedTextService, umbracoApiControllerTypeCollection, eventAggregator)
        {
            _treeEntityPersistence = treeEntityPersistence;
            _menuItemCollectionFactory = menuItemCollectionFactory;
        }

        protected abstract FormulateEntityTypes EntityType { get; }

        /// <summary>
        /// The icon used by the root node.
        /// </summary>
        protected abstract string RootIcon { get; }

        /// <summary>
        /// The icon used by folder nodes.
        /// </summary>
        protected abstract string FolderIcon { get; }

        /// <summary>
        /// The icon used by item nodes.
        /// </summary>
        protected abstract string ItemIcon { get; }
        
        protected override ActionResult<TreeNode> CreateRootNode(FormCollection queryStrings)
        {
            var node = base.CreateRootNode(queryStrings);
            node.Value.Icon = RootIcon;

            return node;
        }

        protected override ActionResult<TreeNodeCollection> GetTreeNodes(string id, FormCollection queryStrings)
        {
            var nodes = new TreeNodeCollection();
            var entities = GetEntities(id);

            foreach (var entity in entities)
            {
                var hasChildren = _treeEntityPersistence.HasChildren(entity.Id);
                var icon = GetIcon(entity);
                var routePath = $"/formulate/formulate/{GetAction(entity)}/{entity.Id:N}";
                var node = CreateTreeNode(entity.Id.ToString(), id, queryStrings, entity.Name, icon, hasChildren, routePath);

                nodes.Add(node);
            }

            return nodes;
        }

        protected virtual string GetAction(IPersistedEntity entity)
        {
            return entity.IsFolder() ? "editFolder" : ItemAction;
        }

        public abstract string ItemAction { get; }

        protected virtual string GetIcon(IPersistedEntity entity)
        {
            return entity.IsFolder() ? FolderIcon : ItemIcon;
        }

        private IReadOnlyCollection<IPersistedEntity> GetEntities(string id)
        {
            if (id.Equals(Constants.System.Root.ToInvariantString()))
            {
                return _treeEntityPersistence.GetRootItems(EntityType);
            }

            if (Guid.TryParse(id, out var parentId))
            {
                return _treeEntityPersistence.GetChildren(parentId);
            }

            return Array.Empty<IPersistedEntity>();
        }

        protected override ActionResult<MenuItemCollection> GetMenuForNode(string id, FormCollection queryStrings)
        {
            return _menuItemCollectionFactory.Create();
        }
    }
}
