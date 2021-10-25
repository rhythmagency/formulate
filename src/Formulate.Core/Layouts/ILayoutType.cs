using Formulate.Core.Types;
using Umbraco.Cms.Core.Composing;

namespace Formulate.Core.Layouts
{
    /// <summary>
    /// A contract for creating a layout type.
    /// </summary>
    public interface ILayoutType : IType, IDiscoverable
    {
        string TypeLabel { get; }

        string Directive { get; }

        ILayout CreateLayout(ILayoutSettings settings);
    }
}
