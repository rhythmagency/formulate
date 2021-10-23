namespace Formulate.Core.FormHandlers
{
    /// <summary>
    /// Creates a <see cref="IFormHandler"/>.
    /// </summary>
    public interface IFormHandlerFactory
    {
        /// <summary>
        /// Creates a form handler for the given settings.
        /// </summary>
        /// <param name="settings">The current settings.</param>
        /// <returns>A <see cref="IFormHandler"/>.</returns>
        /// <remarks>This should be an instance that implements <see cref="AsyncFormHandler"/> or <see cref="FormHandler"/>.</remarks>
        public IFormHandler CreateHandler(IFormHandlerSettings settings);
    }
}