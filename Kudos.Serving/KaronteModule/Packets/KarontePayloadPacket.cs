using System;
using System.Text.Json.Serialization;

namespace Kudos.Serving.KaronteModule.Payloads
{
    public class KarontePayloadPacket
    {
        [JsonInclude][JsonPropertyName("status")]
        internal readonly KaronteStatusPacket? Status;
        [JsonInclude][JsonPropertyName("error")]
        internal KaronteStatusPacket? Error;
        [JsonInclude][JsonPropertyName("data")]
        internal Object? Data;

        internal KarontePayloadPacket() { }
    }
}