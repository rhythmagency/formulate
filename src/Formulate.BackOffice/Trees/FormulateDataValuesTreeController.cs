namespace Formulate.BackOffice.Trees
{
    using Formulate.BackOffice.Attributes;
    using Formulate.BackOffice.Utilities.Trees.DataValues;
    using Umbraco.Cms.Core;
    using Umbraco.Cms.Core.Events;
    using Umbraco.Cms.Core.Services;
    using Umbraco.Cms.Core.Trees;
    using Umbraco.Cms.Web.BackOffice.Trees;
    using FormulateConstants = Constants;

    /// <summary>
    /// The Formulate data values tree controller.
    /// </summary>
    [Tree(FormulateSection.Constants.Alias, FormulateConstants.Trees.DataValues, TreeTitle = "Data Values", TreeGroup = FormulateConstants.TreeGroups.Shared, SortOrder = 2)]
    [FormulateBackOfficePluginController]
    public sealed class FormulateDataValuesTreeController : FormulateEntityTreeController
    {
        /// <inheritdoc />
        protected override string RootNodeIcon => FormulateConstants.Icons.Roots.DataValues;

        /// <summary>
        /// Initializes a new instance of the <see cref="FormulateDataValuesTreeController"/> class.
        /// </summary>
        /// <param name="localizedTextService">The localized text service.</param>
        /// <param name="umbracoApiControllerTypeCollection">The umbraco api controller type collection.</param>
        /// <param name="eventAggregator">The event aggregator.</param>
        public FormulateDataValuesTreeController(IDataValuesEntityTreeUtility dataValuesEntityTreeUtility, ILocalizedTextService localizedTextService, UmbracoApiControllerTypeCollection umbracoApiControllerTypeCollection, IEventAggregator eventAggregator) : base(dataValuesEntityTreeUtility, localizedTextService, umbracoApiControllerTypeCollection, eventAggregator)
        {
        }
    }
}
