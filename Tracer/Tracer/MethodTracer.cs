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
            StackFrame frame = new StackFrame(3);
            var method = frame.GetMethod();
            StartTime = DateTime.Now;
            lock (_locker)
            {
                this.MethodInf = new MethodInformation(method.DeclaringType.ToString(), method.Name, )
                {
                    SortedId = MethodTracer._methodId
                };
                Interlocked.Increment(ref _methodId);
            }
        }

        public MethodInformation StopTrace()
        {
            this.MethodInf.ElapsedTime = DateTime.Now - this.StartTime;
            return this.MethodInf;
        }
    }

    public class MethodInformation : ISortable, ICloneable
    {
        public MethodInformation(string className, string methodName, TimeSpan elapsedTime)
        {
            this.MethodName = methodName;
            this.ClassName = className;
            this.ElapsedTime = elapsedTime;
        }

        public int SortedId
        {
            get;
            set;
        }
        public string ClassName { get; private set; }
        public string MethodName { get; private set; }
        public TimeSpan ElapsedTime { get; private set; }
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
