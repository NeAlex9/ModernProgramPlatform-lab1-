using System;
using System.Diagnostics;
using System.Threading;

namespace NTracer.Tracer
{
    public class MethodTracer
    {
        public DateTime StartTime { get; private set; }
        public DateTime StopTime { get; private set; }

        public void StartTrace()
        {
            this.StartTime = DateTime.Now;
            /*StackFrame frame = new StackFrame(3);
            var method = frame.GetMethod();
            StartTime = DateTime.Now;
            lock (_locker)
            {
                this.MethodInf = new MethodInformation(method.DeclaringType.ToString(), method.Name, )
                {
                    SortedId = MethodTracer._methodId
                };
                Interlocked.Increment(ref _methodId);
            }*/
        }

        public void StopTrace()
        {
            this.StopTime = DateTime.Now;
        }

        public TimeSpan GetElapsedTime()
        {
            return this.StopTime - this.StartTime;
        }
    }
}
