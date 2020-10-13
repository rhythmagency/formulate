namespace formulate.app.CollectionBuilders
{
    using System.Collections.Generic;

    using formulate.app.Forms;

    using Umbraco.Core.Composing;

    /// <inheritdoc />
    public sealed class FormHandlerTypeCollection : BuilderCollectionBase<IFormHandlerType>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FormHandlerTypeCollection"/> class.
        /// </summary>
        /// <param name="items">
        /// The items.
        /// </param>
        public FormHandlerTypeCollection(IEnumerable<IFormHandlerType> items)
            : base(items)
        {
        }
    }
}
