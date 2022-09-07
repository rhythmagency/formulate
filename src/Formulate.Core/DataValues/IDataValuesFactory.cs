namespace Formulate.Core.DataValues
{
    // Namespaces.
    using Types;

    /// <summary>
    /// Creates a <see cref="IDataValues"/>.
    /// </summary>
    public interface IDataValuesFactory
        : IEntityFactory<PersistedDataValues, IDataValues>
    {
    }
}