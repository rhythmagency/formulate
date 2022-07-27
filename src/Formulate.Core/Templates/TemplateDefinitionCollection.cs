using System;
using System.Collections.Generic;
using Umbraco.Cms.Core.Composing;

namespace Formulate.Core.Templates
{
    /// <inheritdoc />
    public sealed class TemplateDefinitionCollection : BuilderCollectionBase<ITemplateDefinition>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TemplateDefinitionCollection"/> class.
        /// </summary>
        /// <param name="items">
        /// The items.
        /// </param>
        public TemplateDefinitionCollection(Func<IEnumerable<ITemplateDefinition>> items)
            : base(items)
        {
        }
    }
}
