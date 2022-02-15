﻿using Formulate.BackOffice.Attributes;
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
    [Tree(FormulateSection.Constants.Alias, Constants.Alias, TreeTitle = "Validation Library", SortOrder = 3)]
    [FormulateBackOfficePluginController]
    public sealed class FormulateValidationsTreeController : FormulateTreeController
    {
        public static class Constants
        {
            public const string Alias = "validations";

            public const string RootNodeIcon = "icon-formulate-validations";

            public const string FolderNodeIcon = "icon-formulate-validation-group";

            public const string ItemNodeIcon = "icon-formulate-validation";
        }

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
        protected override string RootNodeIcon => Constants.RootNodeIcon;

        /// <inheritdoc />
        protected override string FolderNodeIcon => Constants.FolderNodeIcon;

        /// <inheritdoc />
        protected override string ItemNodeIcon => Constants.ItemNodeIcon;

        /// <inheritdoc />
        protected override ActionResult<MenuItemCollection> GetMenuForRoot(FormCollection queryStrings)
        {
            var menuItemCollection = MenuItemCollectionFactory.Create();

            menuItemCollection.AddCreateDialogMenuItem(LocalizedTextService);
            menuItemCollection.AddRefreshMenuItem(LocalizedTextService);

            return menuItemCollection;
        }

        /// <inheritdoc />
        protected override ActionResult<MenuItemCollection> GetMenuForEntity(IPersistedEntity entity, FormCollection queryStrings)
        {
            var menuItemCollection = MenuItemCollectionFactory.Create();

            if (entity is PersistedFolder)
            {
                menuItemCollection.AddCreateDialogMenuItem(LocalizedTextService);
                menuItemCollection.AddDeleteDialogMenuItem(LocalizedTextService);
                menuItemCollection.AddMoveDialogMenuItem(LocalizedTextService);
                menuItemCollection.AddRefreshMenuItem(LocalizedTextService);
            }

            if (entity is PersistedValidation)
            {
                menuItemCollection.AddDeleteDialogMenuItem(LocalizedTextService);
                menuItemCollection.AddMoveDialogMenuItem(LocalizedTextService);
            }

            return menuItemCollection;
        }
    }
}