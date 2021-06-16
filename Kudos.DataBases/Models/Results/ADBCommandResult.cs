using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Kudos.DataBases.Models.Results
{
    public class ADBCommandResult
    {
        public TimeSpan ElapsedTime
        {
            get;
            private set;
        }

        public ADBCommandResult(ref Stopwatch oStopwatch)
        {
            ElapsedTime = oStopwatch != null ? oStopwatch.Elapsed : new TimeSpan();
        }
    }
}