using System;
using Kudos.Socketing.Builders;

namespace Kudos.Socketing.Descriptors
{
	public sealed class PingPongProtocolWebSocketBehaviourDescriptor
	{
        internal readonly TimeSpan? Interval;
        internal readonly Boolean HasValidInterval;
        internal readonly Func<Byte[]?>? OnSendCustomPacket;
        internal readonly Boolean HasOnSendCustomPacket;

        internal PingPongProtocolWebSocketBehaviourDescriptor(PingPongProtocolWebSocketBehaviourBuilder pppwsbb)
        {
            Interval = pppwsbb.Interval;
            HasValidInterval = Interval != null && Interval.Value.Seconds > 0;
            HasOnSendCustomPacket = (OnSendCustomPacket = pppwsbb.OnSendCustomPacket) != null;
        }
	}
}

