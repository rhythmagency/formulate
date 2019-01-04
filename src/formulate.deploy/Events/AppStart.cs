namespace formulate.deploy.Events
{

    // Namespaces.
    using app.Entities;
    using app.Resolvers;
    using Controllers;
    using System;
    using System.Collections.Generic;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;
    using Umbraco.Core;
    using Umbraco.Web;
    using Umbraco.Web.Models.Trees;
    using Umbraco.Web.Trees;
    using Umbraco.Web.UI.JavaScript;


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

        /// <summary>
        /// Application started.
        /// </summary>
        protected override void ApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            ServerVariablesParser.Parsing += AddServerVariables;
        }

        #endregion

        #region Event Handlers

        /// <summary>
        /// Adds server variables so routes can be accessed by back office JavaScript.
        /// </summary>
        private void AddServerVariables(object sender, Dictionary<string, object> e)
        {

            // Variables.
            var httpContext = new HttpContextWrapper(HttpContext.Current);
            var routeData = new RouteData();
            var requestContext = new RequestContext(httpContext, routeData);
            var helper = new UrlHelper(requestContext);
            var key = "formulate";


            // Add server variables.
            var newEntries = new Dictionary<string, string>()
            {
                { "StoreEntityToCloud",
                    helper.GetUmbracoApiService<CloudController>(x =>
                        x.StoreEntityToCloud(null)) },
                { "RemoveEntityFromCloud",
                    helper.GetUmbracoApiService<CloudController>(x =>
                        x.RemoveEntityFromCloud(null)) }
            };
            if (e.ContainsKey(key))
            {
                var existing = e[key] as Dictionary<string, string>;
                foreach (var item in newEntries)
                {
                    existing[item.Key] = item.Value;
                }
            }
            else
            {
                e.Add(key, newEntries);
            }

        }

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


                // Is this a non-root entity (e.g., a form, validation, folder, etc.)?
                if (entity != null && entity.GetType() != typeof(EntityRoot))
                {

                    // Add the "Store to Umbraco Cloud" menu item.
                    var path = "/App_Plugins/formulate/menu-actions/storeEntityToCloud.html";
                    var menuItem = new MenuItem()
                    {
                        Alias = "storeEntityToCloud",
                        Icon = "cloud-upload",
                        Name = "Store to Umbraco Cloud",
                        SeperatorBefore = true
                    };
                    menuItem.LaunchDialogView(path, "Store to Umbraco Cloud");
                    items.Add(menuItem);


                    // Add the "Remove from Cloud" menu item.
                    path = "/App_Plugins/formulate/menu-actions/removeEntityFromCloud.html";
                    menuItem = new MenuItem()
                    {
                        Alias = "removeEntityFromCloud",
                        Icon = "rain",
                        Name = "Remove from Cloud"
                    };
                    menuItem.LaunchDialogView(path, "Remove from Umbraco Cloud");
                    items.Add(menuItem);

                }

            }

        }

        #endregion

    }

}