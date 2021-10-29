using Formulate.BackOffice.Attributes;
using Formulate.BackOffice.Persistence;
using Formulate.Core.ConfiguredForms;
using Formulate.Core.Folders;
using Formulate.Core.Forms;
using Formulate.Core.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Trees;
using Umbraco.Cms.Web.BackOffice.Trees;

namespace Formulate.BackOffice.Trees
{
    /// <summary>
    /// The Formulate forms tree controller.
    /// </summary>
    [Tree(FormulateSection.Constants.Alias, "forms", TreeTitle = "Forms", SortOrder = 0)]
    [FormulatePluginController]
    public sealed class FormulateFormsTreeController : FormulateTreeController
    {
        /// <inheritdoc />
        protected override TreeRootTypes TreeRootType => TreeRootTypes.Forms;

        /// <inheritdoc />
        protected override string RootNodeIcon => "icon-formulate-forms";

        /// <inheritdoc />
        protected override string FolderNodeIcon => "icon-formulate-form-group";

        /// <inheritdoc />
        protected override string ItemNodeIcon => "icon-formulate-form";

        /// <inheritdoc />
        protected override string ItemNodeAction => "editForm";

        /// <summary>
        /// Initializes a new instance of the <see cref="FormulateFormsTreeController"/> class.
        /// </summary>
        /// <inheritdoc />
        public FormulateFormsTreeController(ITreeEntityRepository treeEntityRepository, IMenuItemCollectionFactory menuItemCollectionFactory, ILocalizedTextService localizedTextService, UmbracoApiControllerTypeCollection umbracoApiControllerTypeCollection, IEventAggregator eventAggregator) : base(treeEntityRepository, menuItemCollectionFactory, localizedTextService, umbracoApiControllerTypeCollection, eventAggregator)
        {
        }

        /// <inheritdoc />
        protected override string GetNodeAction(IPersistedEntity entity)
        {
            if (entity is PersistedConfiguredForm)
            {
                return "editConfiguredForm";
            }

            return base.GetNodeAction(entity);
        }

        /// <inheritdoc />
        protected override string GetNodeIcon(IPersistedEntity entity)
        {
            if (entity is PersistedConfiguredForm)
            {
                return "icon-formulate-conform";
            }

            return base.GetNodeIcon(entity);
        }

        /// <inheritdoc />
        protected override ActionResult<MenuItemCollection> GetMenuForRoot(FormCollection queryStrings)
        {
            var menuItemCollection = MenuItemCollectionFactory.Create();

            menuItemCollection.AddCreateFormMenuItem(default, LocalizedTextService);
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
                menuItemCollection.AddCreateFormMenuItem(entity.Id, LocalizedTextService);
                menuItemCollection.AddCreateFolderMenuItem(LocalizedTextService);
                menuItemCollection.AddMoveFolderMenuItem(folder, LocalizedTextService);
                menuItemCollection.AddDeleteFolderMenuItem(LocalizedTextService);
            }

            if (entity is PersistedForm form)
            {
                menuItemCollection.AddMoveFormMenuItem(form, LocalizedTextService);
                menuItemCollection.AddDeleteFormMenuItem(LocalizedTextService);
                menuItemCollection.AddCreateConfiguredFormMenuItem(form.Id, LocalizedTextService);
            }

            if (entity is PersistedConfiguredForm)
            {
                menuItemCollection.AddDeleteConfiguredFormMenuItem(LocalizedTextService);
            }
            else
            {
                menuItemCollection.AddRefreshMenuItem(LocalizedTextService);
            }

            return menuItemCollection;
        }
    }
}
