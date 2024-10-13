using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Databases.Results
{
    public class DatabaseBenchmarkResult
    {
        public static readonly DatabaseBenchmarkResult
            Empty;

        static DatabaseBenchmarkResult()
        {
            Empty = new DatabaseBenchmarkResult();
        }

        public TimeSpan ElapsedTimeOnWaiting { get; private set; }
        public TimeSpan ElapsedTimeOnConnecting { get; private set; }
        public TimeSpan ElapsedTimeOnExecuting { get; private set; }

        private readonly Stopwatch
            _swOnConnecting,
            _swOnExecuting,
            _swOnWaiting;

        internal DatabaseBenchmarkResult()
        {
            _swOnWaiting = new Stopwatch();
            _swOnExecuting = new Stopwatch();
            _swOnConnecting = new Stopwatch();
            ElapsedTimeOnWaiting = ElapsedTimeOnConnecting = ElapsedTimeOnExecuting = TimeSpan.Zero;
        }

        internal DatabaseBenchmarkResult StartOnWaiting()
        {
            _swOnWaiting.Start();
            return this;
        }

        internal DatabaseBenchmarkResult StartOnConnecting()
        {
            _swOnConnecting.Start();
            return this;
        }

        internal DatabaseBenchmarkResult StartOnExecuting()
        {
            _swOnExecuting.Start();
            return this;
        }

        //internal DatabaseBenchmarkResult Start()
        //{
        //    return StartOnWaiting().StartOnExecuting();
        //}

        internal DatabaseBenchmarkResult StopOnWaiting()
        {
            if (_swOnWaiting.IsRunning)
            {
                _swOnWaiting.Stop();
                ElapsedTimeOnWaiting = _swOnWaiting.Elapsed;
            }

            return this;
        }

        internal DatabaseBenchmarkResult StopOnConnecting()
        {
            if (_swOnConnecting.IsRunning)
            {
                _swOnConnecting.Stop();
                ElapsedTimeOnConnecting = _swOnConnecting.Elapsed;
            }

            return this;
        }

        internal DatabaseBenchmarkResult StopOnExecuting()
        {
            if (_swOnExecuting.IsRunning)
            {
                _swOnExecuting.Stop();
                ElapsedTimeOnExecuting = _swOnExecuting.Elapsed;
            }

            return this;
        }

        internal DatabaseBenchmarkResult Stop()
        {
            return StopOnWaiting().StopOnConnecting().StopOnExecuting();
        }
    }
}
