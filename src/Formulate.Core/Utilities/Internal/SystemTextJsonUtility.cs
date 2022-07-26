using Formulate.Core.Converters;
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
            if (string.IsNullOrWhiteSpace(value))
            {
                return default;
            }

            var options = new JsonSerializerOptions();
            options.Converters.Add(new FlexibleGuidJsonConverter());

            return JsonSerializer.Deserialize<T>(value, options);
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
