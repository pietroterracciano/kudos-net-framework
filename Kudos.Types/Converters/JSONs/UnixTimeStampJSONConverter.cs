using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Kudos.Types.Converters.JSONs
{
    public class UnixTimeStampJSONConverter : JsonConverter<UnixTimeStamp>
    {
        public override UnixTimeStamp Read(
            ref Utf8JsonReader oUtf8JsonReader,
            Type typeToConvert,
            JsonSerializerOptions oJsonSerializerOptions
        )
        {
            try { return new UnixTimeStamp(oUtf8JsonReader.GetUInt32()); }
            catch { return UnixTimeStamp.GetOrigin(); }
        }

        public override void Write(
            Utf8JsonWriter oUtf8JsonWriter,
            UnixTimeStamp oUnixTimeStamp,
            JsonSerializerOptions oJsonSerializerOptions
        )
        {
            try { oUtf8JsonWriter.WriteNumberValue(oUnixTimeStamp.ToUInt32()); }
            catch { oUtf8JsonWriter.WriteNumberValue(0); }
        }
    }
}