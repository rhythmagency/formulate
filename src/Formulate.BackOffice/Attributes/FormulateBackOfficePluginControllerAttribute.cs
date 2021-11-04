using Umbraco.Cms.Web.Common.Attributes;

namespace Formulate.BackOffice.Attributes
{
    internal sealed class FormulateBackOfficePluginControllerAttribute : PluginControllerAttribute
    {
        public FormulateBackOfficePluginControllerAttribute() : base("FormulateBackoffice")
        {
        }
    }
}
