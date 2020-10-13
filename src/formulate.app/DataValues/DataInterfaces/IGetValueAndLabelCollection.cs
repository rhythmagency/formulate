namespace formulate.app.DataValues.DataInterfaces
{

    // Namespaces.
    using core.Types;
    using System.Collections.Generic;

    /// <summary>
    /// Any data values implementing this interface can return a collection of value and label items.
    /// </summary>
    public interface IGetValueAndLabelCollection
    {
        /// <summary>
        /// Attempts to get a collection of <see cref="ValueAndLabel"/> for the given raw data.
        /// </summary>
        /// <param name="rawData">
        /// The raw data.
        /// </param>
        /// <returns>
        /// A collection of <see cref="ValueAndLabel"/>.
        /// </returns>
        IEnumerable<ValueAndLabel> GetValues(string rawData);
    }
}
