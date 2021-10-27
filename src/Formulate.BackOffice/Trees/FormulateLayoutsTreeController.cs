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


        protected override string RootIcon => "icon-formulate-layouts";

        protected override string FolderIcon => "icon-formulate-layout-group";

        protected override string ItemIcon => "icon-formulate-layout";

        public override string ItemAction => "editLayout";
    }
}