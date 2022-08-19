namespace Formulate.BackOffice.Trees
{
    using Formulate.BackOffice.Attributes;
    using Formulate.BackOffice.Utilities.Trees;
    using Umbraco.Cms.Core;
    using Umbraco.Cms.Core.Events;
    using Umbraco.Cms.Core.Services;
    using Umbraco.Cms.Core.Trees;
    using Umbraco.Cms.Web.BackOffice.Trees;
    using FormulateConstants = Constants;

    /// <summary>
    /// The Formulate forms tree controller.
    /// </summary>
    [Tree(FormulateSection.Constants.Alias, FormulateConstants.Trees.Forms, TreeTitle = "Forms", SortOrder = 0)]
    [FormulateBackOfficePluginController]
    public sealed class FormulateFormsTreeController : FormulateEntityTreeController
    {
        /// <inheritdoc />
        protected override string RootNodeIcon => FormulateConstants.Icons.Roots.Forms;

        /// <summary>
        /// Initializes a new instance of the <see cref="FormulateFormsTreeController"/> class.
        /// </summary>
        /// <inheritdoc />
        public FormulateFormsTreeController(IFormsEntityTreeUtility formsEntityTreeUtility, ILocalizedTextService localizedTextService, UmbracoApiControllerTypeCollection umbracoApiControllerTypeCollection, IEventAggregator eventAggregator) : base(formsEntityTreeUtility, localizedTextService, umbracoApiControllerTypeCollection, eventAggregator)
        {
        }
    }
}
