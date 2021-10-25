using Formulate.Core.Types;

namespace Formulate.Core.DataValues
{
    /// <summary>
    /// Creates a <see cref="IDataValues"/>.
    /// </summary>
    public interface IAsyncDataValuesFactory : IAsyncEntityFactory<IDataValuesSettings, IDataValues>
    {
    }
}
