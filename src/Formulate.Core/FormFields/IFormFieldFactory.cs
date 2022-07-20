namespace Formulate.Core.FormFields
{
    // Namespaces.
    using Formulate.Core.Types;

    /// <summary>
    /// Creates a <see cref="IFormField"/>.
    /// </summary>
    public interface IFormFieldFactory
        : IEntityFactory<IFormFieldSettings, IFormField>
    {
    }
}