namespace Formulate.Core.Utilities.Internal
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    /// <summary>
    /// An implementation of <see cref="IJsonSerializer"/> that uses <see cref="JsonConvert"/>.
    /// </summary>
    internal sealed class NewtonsoftJsonSerializer : IJsonSerializer
    {
        private readonly JsonSerializerSettings _settings = new JsonSerializerSettings()
        {
            Formatting = Formatting.None,
            ContractResolver = new DefaultContractResolver()
        };

        /// <inheritdoc />
        public T Deserialize<T>(string value)
        {
            return JsonConvert.DeserializeObject<T>(value, _settings);
        }

        /// <inheritdoc />
        public string Serialize(object value)
        {
            return JsonConvert.SerializeObject(value, _settings);
        }
    }
}
