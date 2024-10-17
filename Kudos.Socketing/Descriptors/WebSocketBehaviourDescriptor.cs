using System;
using Kudos.Socketing.Builders;
using System.Net.WebSockets;
using System.Threading;
using Kudos.Socketing.Packets;
using Kudos.Socketing.Interfaces;

namespace Kudos.Socketing.Descriptors
{
	public sealed class WebSocketBehaviourDescriptor
	{
        internal readonly UInt16? ReadBufferSize;
        internal readonly Boolean HasValidReadBufferSize;
        internal readonly WebSocket? WebSocket;
        internal readonly Boolean HasWebSocket;
        internal readonly CancellationToken CancellationToken;
        internal readonly PingPongProtocolWebSocketBehaviourDescriptor PingPongProtocolBehaviourDescriptor;

        internal readonly Action<IActionableWebSocketBehaviour, Byte[]>? OnReceivePacket;
        internal readonly Boolean HasOnReceivePacket;

        internal WebSocketBehaviourDescriptor
        (
            WebSocketBehaviourBuilder wsbb
        )
        {
            HasOnReceivePacket = (OnReceivePacket = wsbb.OnReceivePacket) != null;
            CancellationToken = wsbb.CancellationToken != null ? wsbb.CancellationToken.Value : CancellationToken.None;
            ReadBufferSize = wsbb.ReadBufferSize;
            HasValidReadBufferSize = ReadBufferSize != null && ReadBufferSize > 0;
            HasWebSocket = (WebSocket = wsbb.WebSocket) != null;
            PingPongProtocolBehaviourDescriptor = new PingPongProtocolWebSocketBehaviourDescriptor(wsbb.PingPongProtocolBehaviourBuilder);
        }
	}
}

