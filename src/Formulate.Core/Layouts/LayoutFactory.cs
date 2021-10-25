using System;
using Formulate.Core.Types;

namespace Formulate.Core.Layouts
{
    /// <summary>
    /// The default implementation of <see cref="ILayoutFactory"/> using the <see cref="LayoutDefinitionCollection"/>.
    /// </summary>
    public sealed class LayoutFactory : ILayoutFactory
    {
        /// <summary>
        /// The layout definitions.
        /// </summary>
        private readonly LayoutDefinitionCollection _layoutDefinitions;

        /// <summary>
        /// Initializes a new instance of the <see cref="LayoutFactory"/> class.
        /// </summary>
        /// <param name="layoutDefinitions">The layout definitions.</param>
        public LayoutFactory(LayoutDefinitionCollection layoutDefinitions)
        {
            _layoutDefinitions = layoutDefinitions;
        }

        /// <inheritdoc />
        /// <exception cref="ArgumentNullException">The provided settings are null.</exception>
        public ILayout CreateLayout(ILayoutSettings settings)
        {
            if (settings is null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            var foundLayoutDefinition = _layoutDefinitions.FirstOrDefault(settings.DefinitionId);

            return foundLayoutDefinition?.CreateLayout(settings);
        }
    }
}