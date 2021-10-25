namespace Formulate.Core.Types
{
    /// <summary>
    /// A contract for creating a factory which creates a <typeparamref name="TEntity"/> from a <typeparamref name="TSettings"/>.
    /// </summary>
    /// <typeparam name="TSettings">The type of the settings.</typeparam>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <remarks>This should only be used for readonly operations not persistence.</remarks>
    public interface IEntityFactory<in TSettings, out TEntity> where TSettings : IEntitySettings where TEntity : IEntity
    {
        /// <summary>
        /// Creates a new instance which implements <typeparamref name="TEntity"/>.
        /// </summary>
        /// <param name="settings">The current settings.</param>
        /// <returns>A <typeparamref name="TEntity"/>.</returns>
        TEntity Create(TSettings settings);
    }
}
