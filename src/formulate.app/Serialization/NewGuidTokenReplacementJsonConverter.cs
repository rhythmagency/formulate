namespace formulate.app.Serialization
{
    using System;

    using Newtonsoft.Json;

    /// <summary>
    /// A <see cref="JsonConverter"/> that replaces any tokens that match Guid.NewGuid() with a newly generated GUID.
    /// </summary>
    internal sealed class NewGuidTokenReplacementJsonConverter : JsonConverter
    {
        /// <summary>
        /// Gets that this class does not handle serialization.
        /// </summary>
        public override bool CanWrite => false;

        /// <summary>
        /// Gets that this class does handle deserialization.
        /// </summary>
        public override bool CanRead => true;

        /// <summary>
        /// This does nothing (it must be implemented because it is abstract in the base class).
        /// </summary>
        /// <param name="writer">
        /// The writer.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <param name="serializer">
        /// The serializer.
        /// </param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new InvalidOperationException(
                "This class is not intended to be used for serialization.");
        }

        /// <summary>
        /// Deserializes JSON into a string with any Guid.NewGuid() values replaced.
        /// </summary>
        /// <param name="reader">
        /// The reader.
        /// </param>
        /// <param name="objectType">
        /// The object Type.
        /// </param>
        /// <param name="existingValue">
        /// The existing Value.
        /// </param>
        /// <param name="serializer">
        /// The serializer.
        /// </param>
        /// <returns>
        /// An string.
        /// </returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var stringValue = existingValue?.ToString();
            if (string.IsNullOrWhiteSpace(stringValue))
            {
                return string.Empty;
            }

            return stringValue.Replace("{Guid.NewGuid()}", Guid.NewGuid().ToString());
        }

        /// <summary>
        /// Indicates whether or not this class can convert an object of the specified type.
        /// </summary>
        /// <param name="objectType">
        /// The object Type.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(string);
        }
    }
}
