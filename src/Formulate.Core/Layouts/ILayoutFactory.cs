using Formulate.Core.Types;

namespace Formulate.Core.Layouts
{
    /// <summary>
    /// Creates a <see cref="ILayout"/>.
    /// </summary>
    public interface ILayoutFactory : IEntityFactory<ILayoutSettings, ILayout>
    {
    }
}
