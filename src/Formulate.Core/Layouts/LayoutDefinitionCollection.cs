using System;
using System.Collections.Generic;
using Umbraco.Cms.Core.Composing;

namespace Formulate.Core.Layouts
{
    /// <inheritdoc />
    public sealed class LayoutDefinitionCollection : BuilderCollectionBase<ILayoutDefinition>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LayoutDefinitionCollection"/> class.
        /// </summary>
        /// <param name="items">
        /// The items.
        /// </param>
        public LayoutDefinitionCollection(Func<IEnumerable<ILayoutDefinition>> items)
            : base(items)
        {
        }
    }
}
