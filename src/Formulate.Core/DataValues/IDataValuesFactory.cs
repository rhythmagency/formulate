using Formulate.Core.Types;

namespace Formulate.Core.DataValues
{
    /// <summary>
    /// Creates a <see cref="IDataValues"/>.
    /// </summary>
    public interface IDataValuesFactory : IAsyncEntityFactory<IDataValuesSettings, IDataValues>
    {
    }
}
