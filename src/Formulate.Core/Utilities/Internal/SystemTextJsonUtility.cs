using System.Text.Json;

namespace Formulate.Core.Utilities.Internal
{
    /// <summary>
    /// An implementation of <see cref="IJsonUtility"/> that uses <see cref="JsonSerializer"/>.
    /// </summary>
    internal sealed class SystemTextJsonUtility : IJsonUtility
    {
        /// <inheritdoc />
        public T Deserialize<T>(string value)
        {
            return string.IsNullOrWhiteSpace(value) ? default : JsonSerializer.Deserialize<T>(value);
        }

        /// <inheritdoc />
        public string Serialize(object value)
        {
            return JsonSerializer.Serialize(value, new JsonSerializerOptions()
            {
                WriteIndented = true,
            });
        }
    }
}
