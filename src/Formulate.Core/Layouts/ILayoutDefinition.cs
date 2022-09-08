﻿using Formulate.Core.Types;

namespace Formulate.Core.Layouts
{
    /// <summary>
    /// A contract for creating a layout definition.
    /// </summary>
    public interface ILayoutDefinition : IDefinition, IHaveDirective
    {
        /// <summary>
        /// Creates a new instance of a <see cref="ILayout"/>.
        /// </summary>
        /// <param name="entity">The current layout settings.</param>
        /// <returns>A <see cref="ILayout"/>.</returns>
        ILayout CreateLayout(PersistedLayout entity);

        /// <inheritdoc />
        public object GetBackOfficeConfiguration(PersistedLayout entity);
    }
}
