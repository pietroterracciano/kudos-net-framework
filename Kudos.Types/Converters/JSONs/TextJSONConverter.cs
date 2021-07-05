using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Kudos.Types.Converters.JSONs
{
    public class TextJSONConverter : JsonConverter<Text>
    {
        public override Text Read(
            ref Utf8JsonReader oUtf8JsonReader,
            Type typeToConvert,
            JsonSerializerOptions oJsonSerializerOptions
        )
        {
            try { return new Text(oUtf8JsonReader.GetString()); }
            catch { return new Text(); }
        }

        public override void Write(
            Utf8JsonWriter oUtf8JsonWriter,
            Text oText,
            JsonSerializerOptions oJsonSerializerOptions
        )
        {
            try { oUtf8JsonWriter.WriteStringValue(oText.ToString()); }
            catch { oUtf8JsonWriter.WriteStringValue(""); }
        }
    }
}