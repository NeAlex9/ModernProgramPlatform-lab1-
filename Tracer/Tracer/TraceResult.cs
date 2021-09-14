using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NTracer.Tracer
{
    public class TraceResult
    {
        public List<ThreadNode> Threads { get; }

        public TraceResult(List<ThreadNode> threads)
        {
            this.Threads = threads;
        }

    }

    public class ThreadInformation
    {
        public int Id { get; }
        public List<MethodInformation> MethodsInf { get; }
        public TimeSpan TotalMethodsTime { get; set; }

        public ThreadInformation(List<MethodInformation> methodInf, TimeSpan totalMethodsTime)
        {
            this.Id = Thread.CurrentThread.ManagedThreadId;
            this.MethodsInf = methodInf;
            this.TotalMethodsTime = totalMethodsTime;
        }
    }

    public class MethodInformation
    {
        public MethodInformation(string className, string methodName, TimeSpan elapsedTime, List<MethodInformation> childMethods)
        {
            this.MethodName = methodName;
            this.ClassName = className;
            this.ElapsedTime = elapsedTime;
            this.ChildMethods = childMethods;
        }

        public string ClassName { get; }
        public string MethodName { get; }
        public TimeSpan ElapsedTime { get; }
        public List<MethodInformation> ChildMethods { get; }
    }
}
