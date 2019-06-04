namespace formulate.app.CollectionBuilders
{
    using System.Collections.Generic;

    using formulate.app.Forms;

    using Umbraco.Core.Composing;

    public sealed class FormHandlerTypeCollection : BuilderCollectionBase<IFormHandlerType>
    {
        public FormHandlerTypeCollection(IEnumerable<IFormHandlerType> items)
            : base(items)
        {
        }
    }
}
