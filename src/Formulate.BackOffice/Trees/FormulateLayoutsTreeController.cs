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
    [Tree(FormulateSection.Constants.Alias, "layouts", TreeTitle = "Layouts", SortOrder = 1)]
    [FormulatePluginController]
    public sealed class FormulateLayoutsTreeController : FormulateTreeController
    {
        public FormulateLayoutsTreeController(ITreeEntityPersistence treeEntityPersistence, IMenuItemCollectionFactory menuItemCollectionFactory, ILocalizedTextService localizedTextService, UmbracoApiControllerTypeCollection umbracoApiControllerTypeCollection, IEventAggregator eventAggregator) : base(treeEntityPersistence, menuItemCollectionFactory, localizedTextService, umbracoApiControllerTypeCollection, eventAggregator)
        {
        }

        protected override FormulateEntityTypes EntityType => FormulateEntityTypes.Layouts;


        protected override string RootNodeIcon => "icon-formulate-layouts";

        protected override string FolderNodeIcon => "icon-formulate-layout-group";

        protected override string ItemNodeIcon => "icon-formulate-layout";

        protected override string ItemNodeAction => "editLayout";
    }
}