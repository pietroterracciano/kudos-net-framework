using System;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Kudos.Utils;

namespace Kudos.Socketing.Interfaces
{
	public interface IActionableWebSocketBehaviour
    {
        //public Task SendPacketAsync(Object? o);
        //public Task SendPacketAsync(Object? o, JsonSerializerOptions? jsonso);
        //public Task SendPacketAsync(String? s);
        //public Task SendPacketAsync(String? s, Encoding? enc);
        public void BlinkOnPongReceived();
        public Task SendPacketAsync(Byte[]? ba);
    }
}