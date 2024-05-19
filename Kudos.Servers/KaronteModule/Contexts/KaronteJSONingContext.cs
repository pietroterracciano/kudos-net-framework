using Kudos.Utils;
using System;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Text.Encodings.Web;

namespace Kudos.Servers.KaronteModule.Contexts
{
    public sealed class KaronteJSONingContext : AKaronteChildContext
    {
        private static readonly JsonSerializerOptions __oJsonSerializerOptions = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = false,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        };

        public KaronteJSONingContext(ref KaronteContext kc) : base(ref kc) { }


        public String? Serialize(object? o)
        {
            return JSONUtils.Serialize(o, __oJsonSerializerOptions);
        }

        public ObjectType? Deserialize<ObjectType>(dynamic? dnm)
        {
            return JSONUtils.Deserialize<ObjectType>(dnm, __oJsonSerializerOptions);
        }
    }
}