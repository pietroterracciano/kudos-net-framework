using Kudos.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Types
{
    public class MonitorizedObject
        : TokenizedObject
    {
        private readonly Object _lck;

        public MonitorizedObject() { _lck = new object(); }

        protected void _TryAcquireMonitor() { ThreadingUtils.TryAcquireMonitor(_lck); }
        protected void _TryAcquireMonitor(int iTimeout) { ThreadingUtils.TryAcquireMonitor(_lck, iTimeout); }
        protected void _AcquireMonitor() { ThreadingUtils.AcquireMonitor(_lck); }
        protected void _ReleaseMonitor() { ThreadingUtils.ReleaseMonitor(_lck); }
    }
}