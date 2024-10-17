using System;
using System.Threading.Tasks;

namespace Kudos.Socketing.Interfaces
{
	public interface IStartableWebSocketBehaviour
	{
        public IStoppableWebSocketBehaviour Start();
        public IStoppableWebSocketBehaviour StartAndWait();
    }
}

