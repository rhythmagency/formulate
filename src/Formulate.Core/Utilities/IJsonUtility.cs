namespace Formulate.Core.Utilities
{
    /// <summary>
    /// A utility that handles JSON serialization.
    /// </summary>
    public interface IJsonUtility
    {
        /// <summary>
        /// Deserialize a string to a given definition.
        /// </summary>
        /// <definitionparam name="T">The definition to deserialize to.</definitionparam>
        /// <param name="value">The value.</param>
        /// <returns>A <definitionparamref name="T"/>.</returns>
        T Deserialize<T>(string value);

        /// <summary>
        /// Serialize a object to a JSON string.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>A <see cref="string"/>.</returns>
        string Serialize(object value);
    }
}
