namespace Formulate.Core.FormHandlers
{
    // Namespaces.
    using Types;

    /// <summary>
    /// Settings for creating a form handler.
    /// </summary>
    public interface IFormHandlerSettings : IEntitySettings
    {
        /// <summary>
        /// Gets the alias.
        /// </summary>
        string Alias { get; }

        /// <summary>
        /// Gets a value indicating whether this is enabled.
        /// </summary>
        bool Enabled { get; }
    }
}