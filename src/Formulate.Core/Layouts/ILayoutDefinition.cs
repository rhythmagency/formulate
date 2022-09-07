using Formulate.Core.Types;

namespace Formulate.Core.Layouts
{
    /// <summary>
    /// A contract for creating a layout definition.
    /// </summary>
    public interface ILayoutDefinition : IDefinition
    {
        /// <summary>
        /// Creates a new instance of a <see cref="ILayout"/>.
        /// </summary>
        /// <param name="settings">The current layout settings.</param>
        /// <returns>A <see cref="ILayout"/>.</returns>
        ILayout CreateLayout(PersistedLayout settings);

        /// <inheritdoc />
        public object GetBackOfficeConfiguration(PersistedLayout settings);
    }
}
