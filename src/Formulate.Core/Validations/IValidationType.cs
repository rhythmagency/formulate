using Formulate.Core.Types;
using Umbraco.Cms.Core.Composing;

namespace Formulate.Core.Validations
{
    /// <summary>
    /// A contract for creating a validation type.
    /// </summary>
    public interface IValidationType : IFormulateType, IDiscoverable
    {
    }
}