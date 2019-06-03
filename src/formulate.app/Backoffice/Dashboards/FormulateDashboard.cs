namespace formulate.app.Backoffice.Dashboards
{
    using System;
    using Umbraco.Core.Dashboards;

    internal sealed class FormulateDashboard : IDashboard
    {
        public string Alias => "formulate";

        public string View => "/App_Plugins/formulate/dashboards/introduction.html";

        public string[] Sections => new []{ "formulate" };

        public IAccessRule[] AccessRules => Array.Empty<IAccessRule>();
    }
}
