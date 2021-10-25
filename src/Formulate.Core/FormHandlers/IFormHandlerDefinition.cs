using Formulate.Core.Types;
using Umbraco.Cms.Core.Composing;

namespace Formulate.Core.FormHandlers
{
    /// <summary>
    /// A contract for implementing form handler definition.
    /// </summary>
    /// <remarks>Do not implement this definition directly. Instead implement <see cref="FormHandlerDefinition"/> or <see cref="AsyncFormHandlerDefinition"/>.</remarks>
    public interface IFormHandlerDefinition : IDefinition, IDiscoverable
    {
    }
}
