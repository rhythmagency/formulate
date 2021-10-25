using System;
using Formulate.Core.Types;

namespace Formulate.Core.Layouts
{
    /// <summary>
    /// The default implementation of <see cref="ILayoutFactory"/> using the <see cref="LayoutTypeCollection"/>.
    /// </summary>
    public sealed class LayoutFactory : ILayoutFactory
    {
        /// <summary>
        /// The layout types.
        /// </summary>
        private readonly LayoutTypeCollection layoutTypes;

        /// <summary>
        /// Initializes a new instance of the <see cref="LayoutFactory"/> class.
        /// </summary>
        /// <param name="layoutTypes">The layout types.</param>
        public LayoutFactory(LayoutTypeCollection layoutTypes)
        {
            this.layoutTypes = layoutTypes;
        }

        /// <inheritdoc />
        /// <exception cref="ArgumentNullException">The provided settings are null.</exception>
        public ILayout CreateLayout(ILayoutSettings settings)
        {
            if (settings is null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            var foundLayoutType = layoutTypes.FirstOrDefault(settings.TypeId);

            return foundLayoutType?.CreateLayout(settings);
        }
    }
}