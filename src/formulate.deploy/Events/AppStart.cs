namespace formulate.deploy.Events
{

    // Namespaces.
    using app.Entities;
    using app.Resolvers;
    using System;
    using Umbraco.Core;
    using Umbraco.Web.Models.Trees;
    using Umbraco.Web.Trees;


    /// <summary>
    /// Handles application startup events.
    /// </summary>
    internal class AppStart : ApplicationEventHandler
    {

        #region Overridden Methods

        /// <summary>
        /// Application is starting.
        /// </summary>
        protected override void ApplicationStarting(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            TreeControllerBase.MenuRendering += Handle_RenderContextMenu;
        }

        #endregion

        #region Event Handlers

        /// <summary>
        /// Handles rendering of the context menu.
        /// </summary>
        private void Handle_RenderContextMenu(TreeControllerBase sender, MenuRenderingEventArgs e)
        {

            // Variables.
            var alias = sender.TreeAlias;
            var items = e.Menu.Items;
            var nodeId = e.NodeId;
            var guid = default(Guid);


            // Is this a Formulate context menu?
            if (!"formulate".InvariantEquals(alias))
            {
                return;
            }


            // Is this a GUID (the root has "-1")?
            if (Guid.TryParse(nodeId, out guid))
            {

                // Variables.
                var persistence = EntityPersistence.Current.Manager;
                var entity = persistence.Retrieve(guid);
                var strGuid = guid.ToString("N");


                // Is this a non-root entity (e.g., a form, validation, folder, etc.)?
                if (entity != null && entity.GetType() != typeof(EntityRoot))
                {

                    // Add the "Store to Umbraco Cloud" menu item.
                    var path = "/formulate/formulate/storeEntityToCloud/null";
                    var menuItem = new MenuItem()
                    {
                        Alias = "storeEntityToCloud",
                        Icon = "cloud-upload",
                        Name = "Store to Umbraco Cloud",
                        SeperatorBefore = true
                    };
                    path = path + "?entityGuid=" + strGuid;
                    menuItem.NavigateToRoute(path);
                    items.Add(menuItem);


                    // Add the "Remove from Cloud" menu item.
                    path = "/formulate/formulate/removeEntityFromCloud/null";
                    menuItem = new MenuItem()
                    {
                        Alias = "removeEntityFromCloud",
                        Icon = "rain",
                        Name = "Remove from Cloud"
                    };
                    path = path + "?entityGuid=" + strGuid;
                    menuItem.NavigateToRoute(path);
                    items.Add(menuItem);

                }

            }

        }

        #endregion

    }

}