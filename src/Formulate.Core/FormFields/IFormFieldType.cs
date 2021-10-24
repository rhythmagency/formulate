using Formulate.Core.Types;
using Umbraco.Cms.Core.Composing;

namespace Formulate.Core.FormFields
{
    /// <summary>
    /// A contract for implementing a form field type.
    /// </summary>
    public interface IFormFieldType : IType, IDiscoverable
    {
        /// <summary>
        /// Gets the type label.
        /// </summary>
        string TypeLabel { get; }

        /// <summary>
        /// Gets the icon.
        /// </summary>
        string Icon { get; }

        /// <summary>
        /// Gets the directive.
        /// </summary>
        string Directive { get; }

        /// <summary>
        /// Gets a value indicating whether this field type is transitory.
        /// </summary>
        bool IsTransitory { get; }

        /// <summary>
        /// Gets a value indicating whether this field type is server side only.
        /// </summary>
        bool IsServerSideOnly { get; }

        /// <summary>
        /// Gets a value indicating whether this field type is hidden.
        /// </summary>
        bool IsHidden { get; }

        /// <summary>
        /// Gets a value indicating whether this field type is stored.
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
