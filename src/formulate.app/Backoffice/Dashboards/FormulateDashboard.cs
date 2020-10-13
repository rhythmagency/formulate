namespace formulate.app.Backoffice.Dashboards
{
    using System;
    using Umbraco.Core.Dashboards;

    /// <summary>
    /// The main <see cref="IDashboard"/> used by Formulate in the Umbraco backoffice.
    /// </summary>
    internal sealed class FormulateDashboard : IDashboard
    {
        /// <inheritdoc />
        public string Alias => "formulate";

        /// <inheritdoc />
        public string View => "/App_Plugins/formulate/dashboards/introduction.html";

        /// <inheritdoc />
        public string[] Sections => new[] { "formulate" };

        /// <inheritdoc />
        public IAccessRule[] AccessRules => Array.Empty<IAccessRule>();
    }
}
