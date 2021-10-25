using Formulate.Core.Types;

namespace Formulate.Core.Layouts
{
    /// <summary>
    /// A contract for creating a layout definition.
    /// </summary>
    public interface ILayoutDefinition : IDefinition
    {
        string Directive { get; }

        ILayout CreateLayout(ILayoutSettings settings);
    }
}
