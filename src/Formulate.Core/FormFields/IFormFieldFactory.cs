using Formulate.Core.Types;

namespace Formulate.Core.FormFields
{
    /// <summary>
    /// Creates a <see cref="IFormField"/>.
    /// </summary>
    public interface IFormFieldFactory : IAsyncEntityFactory<IFormFieldSettings, IFormField>
    {
    }
}
