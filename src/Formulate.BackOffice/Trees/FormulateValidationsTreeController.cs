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
    [Tree(FormulateSection.Constants.Alias, "validations", TreeTitle = "Validation Library", SortOrder = 3)]
    [FormulatePluginController]
    public sealed class FormulateDataValuesTreeController : FormulateTreeController
    {
        public FormulateDataValuesTreeController(ITreeEntityPersistence treeEntityPersistence, IMenuItemCollectionFactory menuItemCollectionFactory, ILocalizedTextService localizedTextService, UmbracoApiControllerTypeCollection umbracoApiControllerTypeCollection, IEventAggregator eventAggregator) : base(treeEntityPersistence, menuItemCollectionFactory, localizedTextService, umbracoApiControllerTypeCollection, eventAggregator)
        {
        }

        protected override FormulateEntityTypes EntityType => FormulateEntityTypes.Validations;

        protected override string RootIcon => "icon-formulate-validations";

        protected override string FolderIcon => "icon-formulate-validation-group";

        protected override string ItemIcon => "icon-formulate-validation";

        public override string ItemAction => "editValidation";
    }
}
