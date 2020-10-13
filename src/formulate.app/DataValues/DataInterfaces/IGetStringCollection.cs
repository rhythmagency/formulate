namespace formulate.app.DataValues.DataInterfaces
{

    // Namespaces.
    using System.Collections.Generic;

    /// <summary>
    /// Any data values implementing this interface can return a collection of strings.
    /// </summary>
    public interface IGetStringCollection
    {
        /// <summary>
        /// Attempts to get a collection of strings for the given raw data.
        /// </summary>
        /// <param name="rawData">The raw data.</param>
        /// <returns>A collection of <see cref="string"/>s.</returns>
        IEnumerable<string> GetValues(string rawData);
    }
}
