namespace Formulate.Core.Converters
{
    using System;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    internal class FlexibleGuidJsonConverter : JsonConverter<Guid>
    {
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

        public override void Write(Utf8JsonWriter writer, Guid value, JsonSerializerOptions options)
        {
            throw new NotSupportedException();
        }
    }
}
