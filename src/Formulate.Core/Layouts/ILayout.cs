using Formulate.Core.Types;

namespace Formulate.Core.Layouts
{
    /// <summary>
    /// A contract for creating a layout.
    /// </summary>
    public interface ILayout : IEntity
    {
        /// <summary>
        /// Gets the name.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the Angular JS directive.
        /// </summary>
        string Directive { get; }
    }
}
