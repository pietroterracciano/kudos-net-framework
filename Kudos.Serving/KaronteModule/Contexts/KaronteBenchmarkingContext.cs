using System;
using System.Collections.Generic;
using System.Diagnostics;
using Kudos.Types;

namespace Kudos.Serving.KaronteModule.Contexts
{
    public sealed class KaronteBenchmarkingContext : AKaronteChildContext
    {
        private readonly Metas _m;

        internal KaronteBenchmarkingContext(ref KaronteContext kc) : base(ref kc)
        {
            _m = new Metas();
        }

        public Boolean StartBenchmark(String? s)
        {
            lock(_m)
            {
                if (s == null)
                    return false;

                Stopwatch? sw;
                _GetBenchmark(ref s, out sw);
                if (sw == null) sw = new Stopwatch();
                sw.Reset();
                sw.Start();
                _m.Set(s, sw);
                return true;
            }
        }

        public Boolean StopBenchmark(String? s)
        {
            lock (_m)
            {
                Stopwatch? sw;
                _GetBenchmark(ref s, out sw);
                if (sw == null) return false;
                sw.Stop();
                return true;
            }
        }

        public TimeSpan? GetBenchmarkResult(String? s)
        {
            lock(_m)
            {
                Stopwatch? sw;
                _GetBenchmark(ref s, out sw);
                return sw != null
                    ? sw.Elapsed
                    : null;
            }
        }

        public KeyValuePair<String, TimeSpan>[] GetBenchmarkResults()
        {
            lock (_m)
            {
                KeyValuePair<String, Stopwatch?>[] kvp = _m.Gets<Stopwatch>();
                KeyValuePair<String, TimeSpan>[] kvp0 = new KeyValuePair<string, TimeSpan>[kvp.Length];
                for (int i = 0; i < kvp.Length; i++)
                    kvp0[i] = new KeyValuePair<String, TimeSpan>(kvp[i].Key, kvp[i].Value.Elapsed);

                return kvp0;
            }
        }

        private void _GetBenchmark(ref String? s, out Stopwatch? sw)
        {
            lock(_m)
            {
                sw = _m.Get<Stopwatch>(s);
            }
        }
    }
}