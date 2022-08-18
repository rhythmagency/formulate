namespace Formulate.BackOffice.Trees
{
    // Namespaces.
    using Core.Folders;
    using Core.Persistence;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Persistence;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Umbraco.Cms.Core;
    using Umbraco.Cms.Core.Events;
    using Umbraco.Cms.Core.Services;
    using Umbraco.Cms.Core.Trees;
    using Umbraco.Cms.Web.BackOffice.Trees;
    using Umbraco.Extensions;

    /// <summary>
    /// A base tree controller class for handling Formulate entity trees.
    /// </summary>
    public abstract class FormulateTreeController : TreeController
    {
        /// <summary>
        /// The tree entity repository.
        /// </summary>
        private readonly ITreeEntityRepository _treeEntityRepository;

        /// <summary>
        /// The menu item collection factory.
        /// </summary>
        protected readonly IMenuItemCollectionFactory MenuItemCollectionFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="FormulateTreeController"/> class.
        /// </summary>
        /// <param name="treeEntityRepository">The tree entity repository.</param>
        /// <param name="menuItemCollectionFactory">The menu item collection factory.</param>
        /// <param name="localizedTextService">The localized text service.</param>
        /// <param name="umbracoApiControllerTypeCollection">The umbraco api controller type collection.</param>
        /// <param name="eventAggregator">The event aggregator.</param>
        protected FormulateTreeController(ITreeEntityRepository treeEntityRepository,
            IMenuItemCollectionFactory menuItemCollectionFactory, ILocalizedTextService localizedTextService,
            UmbracoApiControllerTypeCollection umbracoApiControllerTypeCollection, IEventAggregator eventAggregator) :
            base(localizedTextService, umbracoApiControllerTypeCollection, eventAggregator)
        {
            _treeEntityRepository = treeEntityRepository;
            MenuItemCollectionFactory = menuItemCollectionFactory ?? throw new ArgumentNullException(nameof(menuItemCollectionFactory));
        }

        /// <summary>
        /// Gets the tree root type.
        /// </summary>
        protected abstract TreeRootTypes TreeRootType { get; }

        /// <summary>
        /// Gets the root node icon.
        /// </summary>
        protected abstract string RootNodeIcon { get; }
        
        /// <summary>
        /// Gets the folder node icon.
        /// </summary>
        protected abstract string FolderNodeIcon { get; }

        /// <summary>
        /// Gets the item node icon.
        /// </summary>
        protected abstract string ItemNodeIcon { get; }

        /// <inheritdoc />
        protected override ActionResult<TreeNode?> CreateRootNode(FormCollection queryStrings)
        {
            ActionResult<TreeNode?> rootResult = base.CreateRootNode(queryStrings);
            if (rootResult.Result is not null)
            {
                return rootResult;
            }

            var root = rootResult.Value;

            if (root is not null)
            {
                root.Icon = RootNodeIcon;
            }

            return root;
        }

        /// <inheritdoc />
        protected override ActionResult<TreeNodeCollection> GetTreeNodes(string id, FormCollection queryStrings)
        {
            var nodes = new TreeNodeCollection();
            var entities = GetEntities(id);
            var isFolderOnly = queryStrings["foldersonly"].ToString().IsNullOrWhiteSpace() == false && queryStrings["foldersonly"].ToString() == "1";
            var filteredEntities = isFolderOnly ? entities.OfType<PersistedFolder>().ToArray() : entities;
            Func<IPersistedEntity, bool>? hasChildrenFilter = isFolderOnly ? (entity) => entity is PersistedFolder : default;

            foreach (var entity in filteredEntities)
            {
                var hasChildren = _treeEntityRepository.HasChildren(entity.Id, hasChildrenFilter);
                var metaData = GetNodeMetaData(entity);
                var node = CreateTreeNode(entity.BackOfficeSafeId(), id, queryStrings, entity.Name, metaData.Icon, hasChildren);

                if (metaData.IsLegacy)
                {
                    node.SetNotPublishedStyle();
                }

                node.Path = entity.TreeSafePathString();
                node.NodeType = entity.EntityType().ToString();

                // Set additional data so it is readily available to the frontend.
                node.AdditionalData["NodeId"] = entity.Id;
                node.AdditionalData["NodeName"] = entity.Name;
                node.AdditionalData["IsLegacy"] = metaData.IsLegacy;

                SetAdditionalNodeData(entity, node.AdditionalData);

                nodes.Add(node);
            }

            return nodes;
        }
        
        /// <inheritdoc />
        protected sealed override ActionResult<MenuItemCollection> GetMenuForNode(string id, FormCollection queryStrings)
        {
            if (id.Equals(Constants.System.Root.ToInvariantString()))
            {
                return GetMenuForRoot(queryStrings);
            }

            if (Guid.TryParse(id, out var entityId))
            {
                var entity = _treeEntityRepository.Get(entityId);
                if (entity is not null)
                {
                    return GetMenuForEntity(entity, queryStrings);
                }
            }

            return MenuItemCollectionFactory.Create();
        }

        /// <summary>
        /// Creates a menu for a given entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="queryStrings">The query strings for the current request.</param>
        /// <returns>A <see cref="ActionResult{MenuItemCollection}"/>.</returns>
        protected virtual ActionResult<MenuItemCollection> GetMenuForEntity(IPersistedEntity entity, FormCollection queryStrings)
        {
            return MenuItemCollectionFactory.Create();
        }
        
        /// <summary>
        /// Creates a menu for the root node.
        /// </summary>
        /// <param name="queryStrings">The query strings for the current request.</param>
        /// <returns>A <see cref="ActionResult{MenuItemCollection}"/>.</returns>
        protected virtual ActionResult<MenuItemCollection> GetMenuForRoot(FormCollection queryStrings)
        {
            return MenuItemCollectionFactory.Create();
        }
        
        /// <summary>
        /// Gets the icon used for the current entity node.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        protected virtual TreeNodeMetaData GetNodeMetaData(IPersistedEntity entity)
        {
            var icon = entity.IsFolder() ? FolderNodeIcon : ItemNodeIcon;

            return new TreeNodeMetaData(icon);
        }

        /// <summary>
        /// Sets additional data to be passed to the frontend for the given
        /// entity.
        /// </summary>
        /// <param name="entity">
        /// The entity being returned to the frontend as a node.
        /// </param>
        /// <param name="data">
        /// The object to set the additional data on.
        /// </param>
        protected virtual void SetAdditionalNodeData(IPersistedEntity entity,
            IDictionary<string, object?> data)
        {
            // Does nothing here. Derived classes may set additional data.
        }

        /// <summary>
        /// Gets the entities used to populate the tree.
        /// </summary>
        /// <param name="id">The current id.</param>
        /// <returns>A read only collection of <see cref="IPersistedEntity"/>.</returns>
        private IReadOnlyCollection<IPersistedEntity> GetEntities(string id)
        {
            if (id.Equals(Constants.System.Root.ToInvariantString()))
            {
                return _treeEntityRepository.GetRootItems(TreeRootType);
            }

            if (Guid.TryParse(id, out var parentId))
            {
                return _treeEntityRepository.GetChildren(parentId);
            }

            return Array.Empty<IPersistedEntity>();
        }
    }
}