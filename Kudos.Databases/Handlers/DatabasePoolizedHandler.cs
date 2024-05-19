using Kudos.Databases.Interfaces;
using Kudos.Databases.Interfaces.Chains;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Databases.Handlers
{
    public class DatabasePoolizedHandler
    {
        private readonly IDatabaseHandler _oHandler;
        private readonly Object _oLock = new Object();
        private readonly Queue<IDatabaseHandler> _queInStandby, _queInUse;

        internal DatabasePoolizedHandler(IBuildableDatabaseChain bdbc, int i)
        {
            _queInStandby = new Queue<IDatabaseHandler>(i);
            _queInUse = new Queue<IDatabaseHandler>(i);

            lock (_oLock)
            {
                for (int j = 0; j < i; j++)
                {
                    _queInStandby.Enqueue(bdbc.BuildHandler());
                    if (j < 1) _oHandler = _queInStandby.Peek();
                }
            }
        }

        public IDatabaseHandler RequestHandler()
        {
            lock (_oLock)
            {
                if( _queInUse.Count > 0)
                    try
                    {
                        return _queInUse.Dequeue();
                    }
                    catch
                    {
                    }

                if(_queInStandby.Count > 0)
                    try
                    {
                        return _queInStandby.Peek();
                    }
                    catch
                    {
                    }

                return _oHandler;
            }
        }

        private DatabasePoolizedHandler ReleaseHandler(IDatabaseHandler dbh)
        {
            lock (_oLock)
            {
                if (_queInStandby.Count > 0)
                    try
                    {
                        _queInStandby.Enqueue(dbh);
                    }
                    catch
                    {
                    }
            }

            return this;
        }
    }
}
