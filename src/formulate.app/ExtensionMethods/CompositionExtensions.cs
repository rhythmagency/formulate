namespace formulate.app.ExtensionMethods
{
    using formulate.app.CollectionBuilders;

    using Umbraco.Core.Composing;

    /// <summary>
    /// A collection of extension methods to support the <seealso cref="Composition"/> class.
    /// </summary>
    internal static class CompositionExtensions
    {
        /// <summary>
        /// Gets the DataValueKinds builder collection.
        /// </summary>
        /// <param name="composition">The composition.</param>
        /// <returns>The DataValueKinds builder collection.</returns>
        public static DataValueKindCollectionBuilder DataValueKinds(this Composition composition)
        {
            return composition.WithCollectionBuilder<DataValueKindCollectionBuilder>();
        }

        /// <summary>
        /// Gets the FormFieldTypes builder collection.
        /// </summary>
        /// <param name="composition">The composition.</param>
        /// <returns>The FormFieldTypes builder collection.</returns>
        public static FormFieldTypeCollectionBuilder FormFieldTypes(this Composition composition)
        {
            return composition.WithCollectionBuilder<FormFieldTypeCollectionBuilder>();
        }

        /// <summary>
        /// Gets the FormHandlerTypes builder collection.
        /// </summary>
        /// <param name="composition">The composition.</param>
        /// <returns>The FormHandlerTypes builder collection.</returns>
        public static FormHandlerTypeCollectionBuilder FormHandlerTypes(this Composition composition)
        {
            return composition.WithCollectionBuilder<FormHandlerTypeCollectionBuilder>();
        }
    }
}
