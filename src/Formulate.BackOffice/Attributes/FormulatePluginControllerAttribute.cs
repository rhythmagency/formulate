using Umbraco.Cms.Web.Common.Attributes;

namespace Formulate.BackOffice.Attributes
{
    internal sealed class FormulatePluginControllerAttribute : PluginControllerAttribute
    {
        public FormulatePluginControllerAttribute() : base("formulate")
        {
        }
    }
}
