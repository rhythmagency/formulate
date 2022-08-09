namespace Formulate.Core.Utilities.Internal
{
    /// <summary>
    /// An implementation of <see cref="IJsonUtility"/> that wraps a <see cref="IJsonSerializer"/> instance.
    /// </summary>
    internal sealed class JsonUtility : IJsonUtility
    {
        /// <summary>
        /// The JSON serializer.
        /// </summary>
        private readonly IJsonSerializer _serializer;

        public JsonUtility(IJsonSerializer serializer)
        {
            _serializer = serializer;
        }

        /// <inheritdoc />
        public T Deserialize<T>(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return default;
            }

            return _serializer.Deserialize<T>(value);
        }

        /// <inheritdoc />
        public string Serialize(object value)
        {
            return _serializer.Serialize(value);
        }
    }
}
