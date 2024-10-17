using System;
using System.Threading.Tasks;

namespace Kudos.Socketing.Interfaces
{
	public interface IStoppableWebSocketBehaviour
	{
		public Task StopAsync();
		public void Stop();
	}
}

