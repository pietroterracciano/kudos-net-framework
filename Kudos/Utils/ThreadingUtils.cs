using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Kudos.Utils
{
    public static class ThreadingUtils
    {
        private static readonly Int32 __it = Timeout.Infinite;

        public static void TryAcquireMonitor(Object? o) { TryAcquireMonitor(o, __it); }
        public static void TryAcquireMonitor(Object? o, int iTimeout)
        {
            if (o == null) return;
            Boolean b = false;
            try { Monitor.TryEnter(o, __it, ref b); } catch { }
            if (b) _ReleaseMonitor(ref o);
        }

        public static void AcquireMonitor(Object? o)
        {
            if (o == null) return;
            Boolean b = false;
            try { Monitor.Enter(o, ref b); } catch { }
            if (b) _ReleaseMonitor(ref o);
        }

        public static void ReleaseMonitor(Object? o) { if (o != null) _ReleaseMonitor(ref o); }
        private static void _ReleaseMonitor(ref Object o) { try { Monitor.Exit(o); } catch { } }
    }
}