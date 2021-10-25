using Formulate.Core.Types;
using Umbraco.Cms.Core.Composing;

namespace Formulate.Core.Validations
{
    /// <summary>
    /// A contract for creating a validation definition.
    /// </summary>
    public interface IValidationDefinition : IDefinition, IDiscoverable
    {
        /// <summary>
        /// Gets the name.
        /// </summary>
        string DefinitionLabel { get; }

        /// <summary>
        /// Gets the Angular JS directive.
        /// </summary>
        string Directive { get; }
        
        /// <summary>
        /// Creates a new instance of a <see cref="IValidation"/>.
        /// </summary>
        /// <param name="settings">The current validation settings.</param>
        /// <returns>A <see cref="IValidation"/>.</returns>
        IValidation CreateValidation(IValidationSettings settings);
    }
}
