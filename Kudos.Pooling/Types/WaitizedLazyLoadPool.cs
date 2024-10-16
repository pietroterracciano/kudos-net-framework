using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Kudos.Pooling.Policies;
using Kudos.Utils.Numerics;

namespace Kudos.Pooling.Types
{
	public sealed class WaitizedLazyLoadPool<T>
	{
		private readonly Queue<T>? _que;
        private Boolean? _bIsQueueInitialized;
        private readonly Int32? _iMaxSize, _iWaitingTime;
        //private readonly HashSet<T>? _hs;
        private readonly ILazyLoadPoolPolicy<T>? _llpp;
        private readonly Boolean _bHasPolicy;
		private readonly T _dt;

		public WaitizedLazyLoadPool(ILazyLoadPoolPolicy<T> llpp, UInt16? iWaitingTime, UInt16? iMaxSize)
		{
            _bHasPolicy = (_llpp = llpp) != null;
			if (!_bHasPolicy) return;

            _iMaxSize = Int32Utils.Parse(iMaxSize);
            _iWaitingTime = Int32Utils.Parse(iWaitingTime);

            if (_iMaxSize == null || _iMaxSize < 1) _iMaxSize = _iWaitingTime = null;

            if (_iWaitingTime != null && _iWaitingTime < 1) _iWaitingTime = null;

            _bIsQueueInitialized = false;

            if (_iMaxSize != null)
            {
                //_hs = new HashSet<T>(_iMaxSize.Value);
                _que = new Queue<T>(_iMaxSize.Value);
            }
            else
            {
                //_hs = new HashSet<T>();
                _que = new Queue<T>();
            }

			_dt = default(T);
        }

        public Task<T> AcquireAsync() { return Task.Run(Acquire); }
		public T Acquire()
		{
			if (!_bHasPolicy)
                return _dt;

            _TryWait();

            lock (_que)
            {
                T? o;

                try
                {
                    _que.TryDequeue(out o);
                }
                catch
                {
                    o = _dt;
                }

                if (o != null)
                    return o;

                o = _llpp.OnCreateObject();

                if (Release(o))
                {
                    _bIsQueueInitialized = true;
                    return Acquire();
                }

                return o;
            }
		}

        public Task<Boolean> ReleaseAsync(T? t) { return Task.Run(() => Release(t)); }
        public Boolean Release(T? t)
        {
            if
            (
                !_bHasPolicy
                || !_llpp.OnReturnObject(t)
                //|| !_hs.Contains(t)
            )
                return false;

            lock (_que)
            {
                try
                {
                    _que.Enqueue(t);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        private void _TryWait()
        {
            if
            (
                _iMaxSize == null
                || _iWaitingTime == null
                || !_bIsQueueInitialized.Value
            )
                return;

            while ( _que.Count < 1 ) Thread.Sleep(_iWaitingTime.Value);
        }
    }
}

