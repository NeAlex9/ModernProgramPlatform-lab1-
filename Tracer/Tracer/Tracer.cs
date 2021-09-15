using System;
using System.Collections.Generic;
using System.Threading;
using System.Collections.Immutable;
using System.Linq;

namespace NTracer.Tracer
{
    public interface ITracer
    {
        void StartTrace();

        void StopTrace();

        TraceResult GetTraceResult();
    }

    public class Tracer : ITracer
    {
        private static Object _locker;

        static Tracer()
        {
            _locker = new object();
        }

        public Tracer()
        {
            this.ThreadTracers = new List<ThreadTracer>();
        }

        public List<ThreadTracer> ThreadTracers { get; private set; }

        public void StartTrace()
        {
            ThreadTracer threadTracer = GetNeededThreadTracer();
            if (threadTracer == null)
            {
                threadTracer = new ThreadTracer();
                lock (_locker)
                {
                    this.ThreadTracers.Add(threadTracer);
                }
            }

            threadTracer.StartTrace();
        }

        public void StopTrace()
        {
            ThreadTracer threadTracer = GetNeededThreadTracer();
            threadTracer.StopTrace();
        }

        public TraceResult GetTraceResult()
        {
            lock(_locker)
            {
                var threadResults = new List<ThreadInformation>();
                foreach (var threadTracer in this.ThreadTracers)
                {
                    threadResults.Add(threadTracer.GetThreadResult());
                }

                return new TraceResult(threadResults);
            }
        }

        private ThreadTracer GetNeededThreadTracer()
        {
            lock (_locker)
            {
                foreach (var thread in this.ThreadTracers)
                {
                    if (thread.ThreadId == Thread.CurrentThread.ManagedThreadId)
                    {
                        return thread;
                    }
                }
            }

            return null;
        }
    }
}
