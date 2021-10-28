namespace Formulate.Core.Persistence
{
    /// <summary>
    /// A contract for creating settings required to create a <see cref="RepositoryUtility{TPersistedEntity}"/>.
    /// </summary>
    internal interface IRepositoryUtilitySettings
    {
        /// <summary>
        /// Gets the base path to JSON files.
        /// </summary>
        string BasePath { get; }

        /// <summary>
        /// Gets the extension of JSON files.
        /// </summary>
        string Extension { get; }

        /// <summary>
        /// Gets the wildcard used to match JSON files.
        /// </summary>
        string Wildcard { get; }
    }
}
