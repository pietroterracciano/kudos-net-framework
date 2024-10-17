using System;
using System.Threading.Tasks;

namespace Kudos.Socketing.Listeners
{
	public interface IWebSocketPingPongListener
	{
		public void OnReceive();
		public Task OnSendAsync(WebSocketBehaviour wsb);
	}
}

