namespace Formulate.BackOffice.Trees
{
    using Formulate.BackOffice.Attributes;
    using Formulate.BackOffice.Persistence;
    using Formulate.BackOffice.Utilities.Trees;
    using Umbraco.Cms.Core;
    using Umbraco.Cms.Core.Events;
    using Umbraco.Cms.Core.Services;
    using Umbraco.Cms.Core.Trees;
    using Umbraco.Cms.Web.BackOffice.Trees;
    using FormulateConstants = Constants;

    /// <summary>
    /// The Formulate layouts tree controller.
    /// </summary>
    [Tree(FormulateSection.Constants.Alias, FormulateConstants.Trees.Layouts, TreeTitle = "Layouts", SortOrder = 1)]
    [FormulateBackOfficePluginController]
    public sealed class FormulateLayoutsTreeController : FormulateEntityTreeController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FormulateLayoutsTreeController"/> class.
        /// </summary>
        /// <inheritdoc />
        public FormulateLayoutsTreeController(ILayoutsEntityTreeUtility layoutsEntityTreeUtility, ITreeEntityRepository treeEntityRepository, IMenuItemCollectionFactory menuItemCollectionFactory, ILocalizedTextService localizedTextService, UmbracoApiControllerTypeCollection umbracoApiControllerTypeCollection, IEventAggregator eventAggregator) : base(layoutsEntityTreeUtility, localizedTextService, umbracoApiControllerTypeCollection, eventAggregator)
        {
        }

        /// <inheritdoc />
        protected override string RootNodeIcon => FormulateConstants.Icons.Roots.Layouts;
    }
}
