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

        public TimeSpan ElapsedTimeOnWaiting { get; private set;}
        public TimeSpan ElapsedTimeOnExecution { get; private set; }

        private readonly Stopwatch
            _swOnExecution,
            _swOnWaiting;

        internal DatabaseBenchmarkResult()
        {
            _swOnWaiting = new Stopwatch();
            _swOnExecution = new Stopwatch();
            ElapsedTimeOnWaiting = ElapsedTimeOnExecution = TimeSpan.Zero;
        }

        internal DatabaseBenchmarkResult StartOnWaiting()
        {
            _swOnWaiting.Start();
            return this;
        }

        internal DatabaseBenchmarkResult StartOnExecution()
        {
            _swOnExecution.Start();
            return this;
        }

        internal DatabaseBenchmarkResult Start()
        {
            return StartOnWaiting().StartOnExecution();
        }

        internal DatabaseBenchmarkResult StopOnWaiting()
        {
            if (_swOnWaiting.IsRunning)
            {
                _swOnWaiting.Stop();
                ElapsedTimeOnWaiting = _swOnWaiting.Elapsed;
            }

            return this;
        }

        internal DatabaseBenchmarkResult StopOnExecution()
        {
            if (_swOnExecution.IsRunning)
            {
                _swOnExecution.Stop();
                ElapsedTimeOnExecution = _swOnExecution.Elapsed;
            }

            return this;
        }

        internal DatabaseBenchmarkResult Stop()
        {
            return StopOnWaiting().StopOnExecution();
        }
    }
}
