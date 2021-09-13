using System;
using System.Diagnostics;
using System.Threading;

namespace NTracer.Tracer
{
    public class MethodTracer
    {
        private static Object _locker;

        private static int _methodId;

        private MethodInformation MethodInf { get; set; }

        private DateTime StartTime { get; set; }

        static MethodTracer()
        {
            _locker = new object();
            _methodId = 0;
        }

        public void StartTrace()
        {
            lock (_locker)
            {
                this.MethodInf = new MethodInformation
                {
                    SortedId = MethodTracer._methodId
                };
                Interlocked.Increment(ref _methodId);
            }

            StackFrame frame = new StackFrame(3);
            var method = frame.GetMethod();
            this.MethodInf.ClassName = method.DeclaringType.ToString();
            this.MethodInf.MethodName = method.Name;
            StartTime = DateTime.Now;
        }

        public MethodInformation StopTrace()
        {
            this.MethodInf.ElapsedTime = DateTime.Now - this.StartTime;
            return this.MethodInf;
        }
    }

    public class MethodInformation : ISortable, ICloneable
    {
        public int SortedId
        {
            get;
            set;
        }
        public string ClassName { get; set; }
        public string MethodName { get; set; }
        public TimeSpan ElapsedTime { get; set; }
        public object Clone()
        {
            var inf = new MethodInformation
            {
                ClassName = this.ClassName,
                MethodName = this.MethodName,
                ElapsedTime = this.ElapsedTime
            };

            return inf;
        }
    }

    public interface ISortable
    {
        public int SortedId { get; set; }
    }
}
