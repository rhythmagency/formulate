namespace formulate.app.Backoffice.Dashboards
{
    using System;
    using Umbraco.Core.Dashboards;

    internal sealed class FormulateDeveloperDashboard : IDashboard
    {
        public string Alias => "formulateDeveloper";

        public string View => "/App_Plugins/formulate/dashboards/install.html";

        public string[] Sections => new []{ "settings" };

        public IAccessRule[] AccessRules => Array.Empty<IAccessRule>();
    }
}
