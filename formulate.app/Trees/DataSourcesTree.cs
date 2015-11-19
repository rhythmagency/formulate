namespace formulate.app.Trees
{

    // Namespaces.
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Umbraco.Web.Trees;
    using Umbraco.Web.Models.Trees;
    using System.Net.Http.Formatting;
    using Constants = Umbraco.Core.Constants;
    using Umbraco.Core;


    //TODO: Much to do in this file.
    internal class DataSourcesTree : TreeController
    {
        protected override MenuItemCollection GetMenuForNode(string id, FormDataCollection queryStrings)
        {
            var menu = new MenuItemCollection();
            var rootId = Constants.System.Root.ToInvariantString();
            if (id.InvariantEquals(rootId))
            {

            }
            else
            {
            }
            //TODO: ...
            return null;
        }

        protected override TreeNodeCollection GetTreeNodes(string id, FormDataCollection queryStrings)
        {
            //TODO: ...
            return null;
        }
    }

}