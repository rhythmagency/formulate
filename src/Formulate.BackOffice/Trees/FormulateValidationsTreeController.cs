using Formulate.BackOffice.Attributes;
using Formulate.BackOffice.Persistence;
using Formulate.Core.Folders;
using Formulate.Core.Forms;
using Formulate.Core.Persistence;
using Formulate.Core.Validations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Trees;
using Umbraco.Cms.Web.BackOffice.Trees;
using Umbraco.Cms.Web.Common.Attributes;

namespace Formulate.BackOffice.Trees
{
    /// <summary>
    /// The Formulate validations tree controller.
    /// </summary>
    [Tree(FormulateSection.Constants.Alias, "validations", TreeTitle = "Validation Library", SortOrder = 3)]
    [FormulateBackOfficePluginController]
    public sealed class FormulateValidationsTreeController : FormulateTreeController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FormulateValidationsTreeController"/> class.
        /// </summary>
        /// <inheritdoc />
        public FormulateValidationsTreeController(ITreeEntityRepository treeEntityRepository, IMenuItemCollectionFactory menuItemCollectionFactory, ILocalizedTextService localizedTextService, UmbracoApiControllerTypeCollection umbracoApiControllerTypeCollection, IEventAggregator eventAggregator) : base(treeEntityRepository, menuItemCollectionFactory, localizedTextService, umbracoApiControllerTypeCollection, eventAggregator)
        {
        }

        /// <inheritdoc />
        protected override TreeRootTypes TreeRootType => TreeRootTypes.Validations;

        /// <inheritdoc />
        protected override string RootNodeIcon => "icon-formulate-validations";

        /// <inheritdoc />
        protected override string FolderNodeIcon => "icon-formulate-validation-group";

        /// <inheritdoc />
        protected override string ItemNodeIcon => "icon-formulate-validation";

        /// <inheritdoc />
        protected override ActionResult<MenuItemCollection> GetMenuForRoot(FormCollection queryStrings)
        {
            var menuItemCollection = MenuItemCollectionFactory.Create();

            menuItemCollection.AddCreateValidationMenuItem(default, LocalizedTextService);
            menuItemCollection.AddCreateFolderMenuItem(LocalizedTextService);

            menuItemCollection.AddRefreshMenuItem(LocalizedTextService);

            return menuItemCollection;
        }

        /// <inheritdoc />
        protected override ActionResult<MenuItemCollection> GetMenuForEntity(IPersistedEntity entity, FormCollection queryStrings)
        {
            var menuItemCollection = MenuItemCollectionFactory.Create();

            if (entity is PersistedFolder folder)
            {
                menuItemCollection.AddCreateValidationMenuItem(entity.Id, LocalizedTextService);
                menuItemCollection.AddCreateFolderMenuItem(LocalizedTextService);
                menuItemCollection.AddMoveFolderMenuItem(folder, LocalizedTextService);
                menuItemCollection.AddDeleteFolderMenuItem(LocalizedTextService);

                menuItemCollection.AddRefreshMenuItem(LocalizedTextService);
            }

            if (entity is PersistedValidation validation)
            {
                menuItemCollection.AddMoveValidationMenuItem(validation, LocalizedTextService);
                menuItemCollection.AddDeleteValidationMenuItem(LocalizedTextService);
            }

            return menuItemCollection;
        }
    }
}
