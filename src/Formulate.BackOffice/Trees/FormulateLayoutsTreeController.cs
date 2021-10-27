using Formulate.BackOffice.Attributes;
using Formulate.BackOffice.Persistence;
using Formulate.Core.Persistence;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Trees;
using Umbraco.Cms.Web.BackOffice.Trees;

namespace Formulate.BackOffice.Trees
{
    /// <summary>
    /// The Formulate layouts tree controller.
    /// </summary>
    [Tree(FormulateSection.Constants.Alias, "layouts", TreeTitle = "Layouts", SortOrder = 1)]
    [FormulatePluginController]
    public sealed class FormulateLayoutsTreeController : FormulateTreeController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FormulateLayoutsTreeController"/> class.
        /// </summary>
        /// <inheritdoc />
        public FormulateLayoutsTreeController(ITreeEntityPersistence treeEntityPersistence, IMenuItemCollectionFactory menuItemCollectionFactory, ILocalizedTextService localizedTextService, UmbracoApiControllerTypeCollection umbracoApiControllerTypeCollection, IEventAggregator eventAggregator) : base(treeEntityPersistence, menuItemCollectionFactory, localizedTextService, umbracoApiControllerTypeCollection, eventAggregator)
        {
        }

        /// <inheritdoc />
        protected override FormulateEntityTypes EntityType => FormulateEntityTypes.Layouts;

        /// <inheritdoc />
        protected override string RootNodeIcon => "icon-formulate-layouts";

        /// <inheritdoc />
        protected override string FolderNodeIcon => "icon-formulate-layout-group";

        /// <inheritdoc />
        protected override string ItemNodeIcon => "icon-formulate-layout";

        /// <inheritdoc />
        protected override string ItemNodeAction => "editLayout";
    }
}
