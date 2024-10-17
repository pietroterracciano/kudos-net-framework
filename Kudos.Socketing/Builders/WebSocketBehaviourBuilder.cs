using System;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Threading;
using Kudos.Socketing.Descriptors;
using Kudos.Socketing.Interfaces;
using Kudos.Socketing.Packets;

namespace Kudos.Socketing.Builders
{
	public sealed class WebSocketBehaviourBuilder
    {
        internal readonly PingPongProtocolWebSocketBehaviourBuilder PingPongProtocolBehaviourBuilder;

		internal UInt16? ReadBufferSize;
		internal WebSocket? WebSocket;
		internal CancellationToken? CancellationToken;
        internal Action<IActionableWebSocketBehaviour, Byte[]>? OnReceivePacket;

        public WebSocketBehaviourBuilder SetCancellationToken(CancellationToken? ct)
        {
			CancellationToken = ct;
            return this;
        }

        public WebSocketBehaviourBuilder SetReadBufferSize(UInt16? i)
		{
            ReadBufferSize = i;
			return this;
		}

        public WebSocketBehaviourBuilder SetOnReceive(Action<IActionableWebSocketBehaviour, Byte[]>? act)
        {
            OnReceivePacket = act;
            return this;
        }

        public PingPongProtocolWebSocketBehaviourBuilder GetPingPongProtocolBehaviourBuilder()
		{
			return PingPongProtocolBehaviourBuilder;
		}

        public WebSocketBehaviourBuilder SetWebSocket(WebSocket? ws)
        {
            WebSocket = ws;
            return this;
        }

        internal WebSocketBehaviourBuilder()
		{ 
			PingPongProtocolBehaviourBuilder = new PingPongProtocolWebSocketBehaviourBuilder(this);
		}

		public WebSocketBehaviour Build()
		{
			return new WebSocketBehaviour(new WebSocketBehaviourDescriptor(this));
		}
	}
}

