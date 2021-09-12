using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Threading;

namespace NTracer
{
    public interface ITracer
    {
        void StartTrace();

        void StopTrace();

        void GetTraceResult();
    }

    public class TraceResult
    {
        public TraceResult()
        {

        }

        public List<ThreadInformation> ThreadsInformations { get; }
    }

    public class Tracer : ITracer
    {
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
                this.ThreadTracers.Add(threadTracer);
            }

            threadTracer.StartTrace();
        }

        public void StopTrace()
        {
            ThreadTracer threadTracer = GetNeededThreadTracer();
             threadTracer.StopTrace();
        }

        private ThreadTracer GetNeededThreadTracer()
        {
            foreach (var thread in this.ThreadTracers)
            {
                if (thread.Infornmation.Id == Thread.CurrentThread.ManagedThreadId)
                {
                    return thread;
                }
            }

            return null;
        }

        public void GetTraceResult()
        {

        }
    }
}
