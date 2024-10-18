using System;
using System.Threading.Tasks;

namespace Kudos.Databasing.Interfaces
{
	public interface IPoolizedDatabaseHandler : IDisposable
	{
        public Task DisposeAsync();
        //public void CloseAllConnections();
        //public Task CloseAllConnectionsAsync();
        public IDatabaseHandler Acquire();
        public Task<IDatabaseHandler> AcquireAsync();
        public Boolean Release(IDatabaseHandler? dh);
        public Task<Boolean> ReleaseAsync(IDatabaseHandler? dh);
    }
}

