using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using Kudos.Databases.Chains;
using Kudos.Databases.Descriptors;
using Kudos.Databases.Interfaces;
using Kudos.Databases.Interfaces.Chains;
using Kudos.Databases.Policies;
using Kudos.Databases.Results;
using Kudos.Pooling.Types;
using Kudos.Utils.Numerics;
using Microsoft.Extensions.ObjectPool;

namespace Kudos.Databases.Handlers
{
    public sealed class PoolizedDatabaseHandler : IPoolizedDatabaseHandler
    {
        private readonly WaitizedLazyLoadPool<IDatabaseHandler> _wllop;
        private readonly HashSet<IDatabaseHandler> _hsdh;

        internal PoolizedDatabaseHandler(IBuildableDatabaseChain bdc)
        {
            DatabaseChain dc = bdc as DatabaseChain;
            _wllop = new WaitizedLazyLoadPool<IDatabaseHandler>
            (
                new DatabaseHandlerPoolPolicy(ref bdc),
                40,
                dc._MaximumPoolSize
            );
            _hsdh = new HashSet<IDatabaseHandler>(Int32Utils.NNParse(dc._MaximumPoolSize));
        }

        public void Dispose()
        {
            lock (_hsdh)
            {
                foreach (IDatabaseHandler db in _hsdh)
                {
                    db.Dispose();
                }
            }
        }

        public async Task DisposeAsync()
        {
            Task[] tdra = new Task[_hsdh.Count];
            int i = 0;

            lock (_hsdh)
            {
                foreach (IDatabaseHandler db in _hsdh)
                {
                    tdra[i] = db.DisposeAsync();
                    i++;
                }
            }

            await Task.WhenAll(tdra);
        }

        //public void CloseAllConnections()
        //{
        //    lock (_hsdh)
        //    {
        //        foreach (IDatabaseHandler db in _hsdh)
        //        {
        //            db.CloseConnection();
        //        }
        //    }
        //}

        //public Task CloseAllConnectionsAsync()
        //{
        //    return Task.Run
        //    (
        //        async () =>
        //        {
        //            Task<DatabaseResult>[] tdra = new Task<DatabaseResult>[_hsdh.Count];
        //            int i = 0;

        //            lock (_hsdh)
        //            {
        //                foreach (IDatabaseHandler db in _hsdh)
        //                {
        //                    tdra[i] = db.CloseConnectionAsync();
        //                    i++;
        //                }
        //            }

        //            await Task.WhenAll(tdra);

        //            return Task.CompletedTask;
        //        }
        //    );
        //}

        public async Task<IDatabaseHandler> AcquireAsync()
        {
            IDatabaseHandler dh = await _wllop.AcquireAsync();
            lock (_hsdh) { _hsdh.Add(dh); }
            return dh;
        }
        public IDatabaseHandler Acquire()
        {
            IDatabaseHandler dh = _wllop.Acquire();
            lock (_hsdh) { _hsdh.Add(dh); }
            return dh;
        }

        public async Task<Boolean> ReleaseAsync(IDatabaseHandler? dh)
        {
            return await _wllop.ReleaseAsync(dh);
        }
        public Boolean Release(IDatabaseHandler? dh)
        {
            return _wllop.Release(dh);
        }
    }
}