using System;
using System.Collections.Generic;
using Umbraco.Cms.Core.Composing;

namespace Formulate.Core.Types
{
    /// <inheritdoc />
    public sealed class LayoutTypeCollection : BuilderCollectionBase<ILayoutType>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LayoutTypeCollection"/> class.
        /// </summary>
        /// <param name="items">
        /// The items.
        /// </param>
        public LayoutTypeCollection(Func<IEnumerable<ILayoutType>> items)
            : base(items)
        {
        }
    }
}
