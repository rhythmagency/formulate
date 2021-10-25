using Formulate.Core.Types;

namespace Formulate.Core.FormFields
{
    /// <summary>
    /// A contract for implementing a form field definition.
    /// </summary>
    public interface IFormFieldDefinition : IDefinition
    {
        /// <summary>
        /// Gets the definition label.
        /// </summary>
        string DefinitionLabel { get; }

        /// <summary>
        /// Gets the icon.
        /// </summary>
        string Icon { get; }

        /// <summary>
        /// Gets the directive.
        /// </summary>
        string Directive { get; }

        /// <summary>
        /// Gets a value indicating whether this field definition is transitory.
        /// </summary>
        bool IsTransitory { get; }

        /// <summary>
        /// Gets a value indicating whether this field definition is server side only.
        /// </summary>
        bool IsServerSideOnly { get; }

        /// <summary>
        /// Gets a value indicating whether this field definition is hidden.
        /// </summary>
        bool IsHidden { get; }

        /// <summary>
        /// Gets a value indicating whether this field definition is stored.
        /// </summary>
        bool IsStored { get; }

        /// <summary>
        /// Creates a new instance of a <see cref="IFormField"/>.
        /// </summary>
        /// <param name="settings">The current form settings.</param>
        /// <returns>A <see cref="IFormField"/>.</returns>
        IFormField CreateField(IFormFieldSettings settings);
    }
}
