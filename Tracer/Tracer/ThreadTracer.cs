using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace NTracer.Tracer
{
    public class ThreadTracer
    {
        public Stack<MethodTracer> MethodTracers { get; set; }
        public ThreadInformation Information { get; set; }

        public ThreadTracer()
        {
            this.Information = new ThreadInformation();
            this.MethodTracers = new Stack<MethodTracer>();
        }

        public void StartTrace()
        {
            var stack = new StackTrace();
            var methodTracer = new MethodTracer();
            this.MethodTracers.Push(methodTracer);
            this.Information.InvokedMethods.Add(stack.GetFrame(1).GetMethod().Name);
            this.Information.InvokedMethods.Add(stack.GetFrame(2).GetMethod().Name);
            var frame = stack.GetFrames();
            methodTracer.StartTrace();
        }

        public void StopTrace()
        {
            var methodInf = this.MethodTracers.Pop().StopTrace();
            var stack = new StackTrace();
            var methodTracer = new MethodTracer();
            this.Information.InvokedMethods.Add(stack.GetFrame(1).GetMethod().Name);
            this.Information.MethodsInf.Add(methodInf);
        }
    }

    public class ThreadInformation
    {
        public ThreadInformation()
        {
            this.Id = Thread.CurrentThread.ManagedThreadId;
            this.MethodsInf = new List<MethodInformation>();
            this.InvokedMethods = new List<string>();
        }

        public List<string> InvokedMethods{ get; private set; }

        public int Id { get; }

        public List<MethodInformation> MethodsInf { get; set; }
    }
}
