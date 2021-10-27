using Formulate.BackOffice.Attributes;
using Formulate.BackOffice.Persistence;
using Formulate.Core.DataValues;
using Formulate.Core.Persistence;
using Formulate.Core.Types;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Trees;
using Umbraco.Cms.Web.BackOffice.Trees;

namespace Formulate.BackOffice.Trees
{
    [Tree(FormulateSection.Constants.Alias, "datavalues", TreeTitle = "Data Values", SortOrder = 2)]
    [FormulatePluginController]
    public sealed class FormulateValidationsTreeController : FormulateTreeController
    {
        private readonly DataValuesDefinitionCollection _dataValuesDefinitions;

        public FormulateValidationsTreeController(DataValuesDefinitionCollection dataValuesDefinitions, ITreeEntityPersistence treeEntityPersistence, IMenuItemCollectionFactory menuItemCollectionFactory, ILocalizedTextService localizedTextService, UmbracoApiControllerTypeCollection umbracoApiControllerTypeCollection, IEventAggregator eventAggregator) : base(treeEntityPersistence, menuItemCollectionFactory, localizedTextService, umbracoApiControllerTypeCollection, eventAggregator)
        {
            _dataValuesDefinitions = dataValuesDefinitions;
        }

        protected override string GetNodeIcon(IPersistedEntity entity)
        {
            if (entity is PersistedDataValues dataValuesEntity)
            {
                var definition = _dataValuesDefinitions.FirstOrDefault(dataValuesEntity.DefinitionId);

                if (definition is not null)
                {
                    var icon = definition.Icon;

                    if (string.IsNullOrWhiteSpace(icon) == false)
                    {
                        return icon;
                    }
                }
            }

            return base.GetNodeIcon(entity);
        }


        protected override FormulateEntityTypes EntityType => FormulateEntityTypes.Validations;

        protected override string RootNodeIcon => "icon-formulate-values";

        protected override string FolderNodeIcon => "icon-formulate-value-group";

        protected override string ItemNodeIcon => "icon-formulate-value";

        public override string ItemAction => "editDataValue";
    }
}
