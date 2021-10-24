namespace Formulate.Core.FormFields
{
    /// <summary>
    /// Creates a <see cref="IFormField"/>.
    /// </summary>
    public interface IFormFieldFactory
    {
        /// <summary>
        /// Creates a form field for the given settings.
        /// </summary>
        /// <param name="settings">The current settings.</param>
        /// <returns>A <see cref="IFormField"/>.</returns>
        IFormField CreateField(IFormFieldSettings settings);
    }
}
