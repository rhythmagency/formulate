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
    /// The Formulate validations tree controller.
    /// </summary>
    [Tree(FormulateSection.Constants.Alias, "validations", TreeTitle = "Validation Library", SortOrder = 3)]
    [FormulatePluginController]
    public sealed class FormulateValidationsTreeController : FormulateTreeController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FormulateValidationsTreeController"/> class.
        /// </summary>
        /// <inheritdoc />
        public FormulateValidationsTreeController(ITreeEntityPersistence treeEntityPersistence, IMenuItemCollectionFactory menuItemCollectionFactory, ILocalizedTextService localizedTextService, UmbracoApiControllerTypeCollection umbracoApiControllerTypeCollection, IEventAggregator eventAggregator) : base(treeEntityPersistence, menuItemCollectionFactory, localizedTextService, umbracoApiControllerTypeCollection, eventAggregator)
        {
        }

        /// <inheritdoc />
        protected override FormulateEntityTypes EntityType => FormulateEntityTypes.Validations;

        /// <inheritdoc />
        protected override string RootNodeIcon => "icon-formulate-validations";

        /// <inheritdoc />
        protected override string FolderNodeIcon => "icon-formulate-validation-group";

        /// <inheritdoc />
        protected override string ItemNodeIcon => "icon-formulate-validation";

        /// <inheritdoc />
        protected override string ItemNodeAction => "editValidation";
    }
}
