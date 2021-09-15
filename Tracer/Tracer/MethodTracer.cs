using System;
using System.Diagnostics;
using System.Threading;

namespace NTracer.Tracer
{
    public class MethodTracer
    {
        public Stopwatch Stopwatch { get; }

        public MethodTracer()
        {
            this.Stopwatch = new Stopwatch();
        }

        public void StartTrace()
        {
            this.Stopwatch.Start();
        }

        public void StopTrace()
        {
            this.Stopwatch.Stop();
        }

        public TimeSpan GetElapsedTime()
        {
            return this.Stopwatch.Elapsed;
        }
    }
}
