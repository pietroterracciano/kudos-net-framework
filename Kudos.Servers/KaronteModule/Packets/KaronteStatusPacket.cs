using System;
using System.Net;
using System.Text.Json.Serialization;

namespace Kudos.Servers.KaronteModule.Payloads
{
    public class KaronteStatusPacket
    {
        [JsonInclude][JsonPropertyName("code")]
        public Int32? Code { get; internal set; }
        [JsonInclude][JsonPropertyName("description")]
        public String? Description { get; internal set; }

        internal KaronteStatusPacket() { }
    }
}
