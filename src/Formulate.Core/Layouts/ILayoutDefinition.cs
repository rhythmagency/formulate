using Formulate.Core.Types;
using Umbraco.Cms.Core.Composing;

namespace Formulate.Core.Layouts
{
    /// <summary>
    /// A contract for creating a layout definition.
    /// </summary>
    public interface ILayoutDefinition : IDefinition, IDiscoverable
    {
        string DefinitionLabel { get; }

        string Directive { get; }

        ILayout CreateLayout(ILayoutSettings settings);
    }
}
