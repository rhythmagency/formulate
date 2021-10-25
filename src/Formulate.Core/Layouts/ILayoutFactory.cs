namespace Formulate.Core.Layouts
{
    /// <summary>
    /// Creates a <see cref="ILayout"/>.
    /// </summary>
    public interface ILayoutFactory
    {
        /// <summary>
        /// Creates a layout for the given settings.
        /// </summary>
        /// <param name="settings">The current settings.</param>
        /// <returns>A <see cref="ILayout"/>.</returns>
        ILayout CreateLayout(ILayoutSettings settings);
    }
}
