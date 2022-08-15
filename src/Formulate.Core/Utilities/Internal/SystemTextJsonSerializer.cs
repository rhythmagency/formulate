namespace Formulate.Core.Utilities.Internal
{
    using Formulate.Core.Converters;
    using System.Runtime.Serialization.Json;
    using System.Text.Json;

    /// <summary>
    /// An implementation of <see cref="IJsonSerializer"/> that uses <see cref="JsonSerializer"/>.
    /// </summary>
    internal sealed class SystemTextJsonSerializer : IJsonSerializer
    {
        public T Deserialize<T>(string value)
        {
            var options = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true,
            };

            options.Converters.Add(new FlexibleGuidJsonConverter());

            return JsonSerializer.Deserialize<T>(value, options);
        }

        public string Serialize(object value)
        {
            return JsonSerializer.Serialize(value, new JsonSerializerOptions()
            {
                WriteIndented = true,
            });
        }
    }
}
