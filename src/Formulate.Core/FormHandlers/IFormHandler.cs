namespace Formulate.Core.FormHandlers
{
    // Namespaces.
    using Types;

    /// <summary>
    /// A base contract for creating a Form Handler.
    /// </summary>
    /// <remarks>
    /// Do not implement this definition directly.
    /// Instead implement <see cref="FormHandler"/> or <see cref="AsyncFormHandler"/>.
    /// </remarks>
    public interface IFormHandler : IEntity
    {
        /// <summary>
        /// Gets the alias.
        /// </summary>
        string Alias { get; }

        /// <summary>
        /// Gets the name.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets a value indicating whether this is enabled.
        /// </summary>
        bool Enabled { get; }

        /// <summary>
        /// Gets the icon for the form handler.
        /// </summary>
        string Icon { get; }

        /// <summary>
        /// Gets the directive for the form handler.
        /// </summary>
        string Directive { get; }
    }
}