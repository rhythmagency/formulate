using Formulate.Core.Types;
using Umbraco.Cms.Core.Composing;

namespace Formulate.Core.FormFields
{
    /// <summary>
    /// A contract for implementing form field type.
    /// </summary>
    public interface IFormFieldType : IFormulateType, IDiscoverable
    {
    }
}
