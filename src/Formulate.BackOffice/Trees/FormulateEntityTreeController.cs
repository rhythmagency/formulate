namespace Formulate.BackOffice.Trees
{
    using Formulate.BackOffice.Utilities.Trees;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Umbraco.Cms.Core;
    using Umbraco.Cms.Core.Events;
    using Umbraco.Cms.Core.Services;
    using Umbraco.Cms.Core.Trees;
    using Umbraco.Cms.Web.BackOffice.Trees;
    using Umbraco.Extensions;

    /// <summary>
    /// A base tree controller class for handling Formulate entity trees.
    /// </summary>
    public abstract class FormulateEntityTreeController : TreeController
    {
        /// <summary>
        /// The entity tree utility.
        /// </summary>
        private readonly IEntityTreeUtility _entityTreeUtility;

        /// <summary>
        /// Initializes a new instance of the <see cref="FormulateEntityTreeController"/> class.
        /// </summary>
        /// <param name="localizedTextService">The localized text service.</param>
        /// <param name="umbracoApiControllerTypeCollection">The umbraco api controller type collection.</param>
        /// <param name="eventAggregator">The event aggregator.</param>
        protected FormulateEntityTreeController(IEntityTreeUtility entityTreeUtility,
            ILocalizedTextService localizedTextService,
            UmbracoApiControllerTypeCollection umbracoApiControllerTypeCollection, IEventAggregator eventAggregator) :
            base(localizedTextService, umbracoApiControllerTypeCollection, eventAggregator)
        {
            _entityTreeUtility = entityTreeUtility;
        }

        /// <summary>
        /// Gets the root node icon.
        /// </summary>
        protected abstract string RootNodeIcon { get; }
        
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
            var input = new GetTreeNodesInput(id, queryStrings);
            var entities = _entityTreeUtility.GetTreeNodes(input);
            var nodes = new TreeNodeCollection();
            
            foreach (var entity in entities)
            {
                var node = CreateTreeNode(entity.Id, id, queryStrings, entity.Name, entity.Icon, entity.HasChildren);

                if (entity.IsLegacy)
                {
                    node.SetNotPublishedStyle();
                }

                node.Path = entity.Path;
                node.NodeType = entity.NodeType;

                // Set additional data so it is readily available to the frontend.
                node.AdditionalData["NodeId"] = entity.Id;
                node.AdditionalData["NodeName"] = entity.Name;
                node.AdditionalData["IsLegacy"] = entity.IsLegacy;

                nodes.Add(node);
            }

            return nodes;
        }
        
        /// <inheritdoc />
        protected sealed override ActionResult<MenuItemCollection> GetMenuForNode(string id, FormCollection queryStrings)
        {
            var input = new GetMenuItemsInput(id, queryStrings);

            return _entityTreeUtility.GetMenuItems(input);
        }
    }
}