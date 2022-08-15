namespace Formulate.BackOffice.Attributes
{
    using Umbraco.Cms.Web.Common.Attributes;

    internal sealed class FormulateBackOfficePluginControllerAttribute : PluginControllerAttribute
    {
        public FormulateBackOfficePluginControllerAttribute() : base("FormulateBackoffice")
        {
        }
    }
}
