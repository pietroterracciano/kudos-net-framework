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

        public static void TryEnterMonitor(Object? o) { TryEnterMonitor(o, __it); }
        public static void TryEnterMonitor(Object? o, int iTimeout)
        {
            if (o == null || iTimeout < 1) return;
            //Boolean b = false;
            try { Monitor.TryEnter(o, iTimeout); } catch { }
            //if (b) _ExitMonitor(ref o);
        }

        public static void EnterMonitor(Object? o)
        {
            if (o == null) return;
            //Boolean b = false;
            try { Monitor.Enter(o); } catch { }
            //if (b) _ExitMonitor(ref o);
        }

        public static void ExitMonitor(Object? o) { if(o != null) try { Monitor.Exit(o); } catch { } }
        //private static void _ExitMonitor(ref Object o) { try { Monitor.Exit(o); } catch { } }
    }
}