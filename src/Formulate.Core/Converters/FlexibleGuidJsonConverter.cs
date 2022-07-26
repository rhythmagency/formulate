namespace Formulate.Core.Converters
{
    using System;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    /// <summary>
    /// A flexible JSON Converter for GUID.
    /// </summary>
    /// <remarks>This exists as the default JSON Converter for System.Text.Json does not handled GUID outside of the standard "D" format.</remarks>
    internal sealed class FlexibleGuidJsonConverter : JsonConverter<Guid>
    {
        /// <inheritdoc />
        public override Guid Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TryGetGuid(out var guid))
            {
                return guid;
            }

            var value = reader.GetString();

            if (Guid.TryParse(value, out var valueGuid))
            {
                return valueGuid;
            }

            return Guid.Empty;
        }

        /// <remarks>
        /// This method is currently not supported and should not be used in this scenario.
        /// </remarks>
        /// <inheritdoc />
        public override void Write(Utf8JsonWriter writer, Guid value, JsonSerializerOptions options)
        {
            throw new NotSupportedException();
        }
    }
}
