using System;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Threading;
using Kudos.Utils;
using System.Threading.Tasks;
using Kudos.Utils.Texts;

namespace Kudos.Socketing.Packets
{
	public sealed class WebSocketReceivePacket
	{
		internal readonly WebSocketReceiveResult Result;
		public readonly Byte[] Bytes;

		internal WebSocketReceivePacket(ref WebSocketReceiveResult wsrr, ref Byte[] ba)
		{
            Result = wsrr;
            Bytes = new byte[wsrr.Count];
            Array.Copy(ba, Bytes, Bytes.Length);
        }

		public String? ReadAsString() { return ReadAsString(Encoding.UTF8);}
		public String? ReadAsString(Encoding? enc) { return StringUtils.Parse(Bytes, enc); }
        public T? ReadAsObject<T>() { return ReadAsObject<T>(null); }
        public T? ReadAsObject<T>(JsonSerializerOptions? jsonso) { return JSONUtils.Deserialize<T>(ReadAsString(), jsonso); }
    }
}