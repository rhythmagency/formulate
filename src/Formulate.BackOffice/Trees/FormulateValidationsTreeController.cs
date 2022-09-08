namespace Formulate.BackOffice.Trees
{
    // Namespaces.
    using Attributes;
    using Formulate.BackOffice.Utilities.Trees.Validations;
    using Persistence;
    using Umbraco.Cms.Core;
    using Umbraco.Cms.Core.Events;
    using Umbraco.Cms.Core.Services;
    using Umbraco.Cms.Core.Trees;
    using Umbraco.Cms.Web.BackOffice.Trees;
    using FormulateConstants = Constants;

    /// <summary>
    /// The Formulate validations tree controller.
    /// </summary>
    [Tree(FormulateSection.Constants.Alias, FormulateConstants.Trees.Validations, TreeTitle = "Validation Library", TreeGroup = FormulateConstants.TreeGroups.Shared, SortOrder = 3)]
    [FormulateBackOfficePluginController]
    public sealed class FormulateValidationsTreeController : FormulateEntityTreeController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FormulateValidationsTreeController"/> class.
        /// </summary>
        /// <inheritdoc />
        public FormulateValidationsTreeController(IValidationsEntityTreeUtility validationsEntityTreeUtility , ITreeEntityRepository treeEntityRepository, IMenuItemCollectionFactory menuItemCollectionFactory, ILocalizedTextService localizedTextService, UmbracoApiControllerTypeCollection umbracoApiControllerTypeCollection, IEventAggregator eventAggregator) : base(validationsEntityTreeUtility, localizedTextService, umbracoApiControllerTypeCollection, eventAggregator)
        {
        }

        /// <inheritdoc />
        protected override string RootNodeIcon => FormulateConstants.Icons.Roots.Validations;
    }
}