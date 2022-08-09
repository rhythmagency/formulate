namespace Formulate.Core.Utilities
{
    /// <summary>
    /// A wrapper for a JSON serializer (e.g. System.Text.Json or Newtonsoft.Json).
    /// </summary>
    /// <remarks>This should not be used directly, use <see cref="IJsonUtility"/> instead.</remarks>
    internal interface IJsonSerializer
    {
        T Deserialize<T>(string value);

        string Serialize(object value);
    }
}
