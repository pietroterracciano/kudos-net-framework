using Kudos.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Types
{
    public class MonitorizedObject
    {
        private readonly Object _lck;

        public MonitorizedObject() { _lck = new object(); }

        protected void _TryEnterMonitor() { ThreadingUtils.TryEnterMonitor(_lck); }
        protected void _TryEnterMonitor(int iTimeout) { ThreadingUtils.TryEnterMonitor(_lck, iTimeout); }
        protected void _EnterMonitor() { ThreadingUtils.EnterMonitor(_lck); }
        protected void _ExitMonitor() { ThreadingUtils.ExitMonitor(_lck); }
    }
}