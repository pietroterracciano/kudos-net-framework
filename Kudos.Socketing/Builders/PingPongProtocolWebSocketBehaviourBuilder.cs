using System;
namespace Kudos.Socketing.Builders
{
	public sealed class PingPongProtocolWebSocketBehaviourBuilder
	{
		internal readonly WebSocketBehaviourBuilder BehaviourBuilder;
        internal Func<Byte[]?>? OnSendCustomPacket;
        internal TimeSpan? Interval;

        public PingPongProtocolWebSocketBehaviourBuilder SetInterval(TimeSpan? ts)
        {
            Interval = ts;
            return this;
        }

        public PingPongProtocolWebSocketBehaviourBuilder SetOnSendCustomPacket(Func<Byte[]?>? fnc)
        {
            OnSendCustomPacket = fnc;
            return this;
        }

        public WebSocketBehaviourBuilder GetBehaviourBuilder()
        {
            return BehaviourBuilder;
        }

        public PingPongProtocolWebSocketBehaviourBuilder(WebSocketBehaviourBuilder wsbb)
		{
            BehaviourBuilder = wsbb;
		}
	}
}

