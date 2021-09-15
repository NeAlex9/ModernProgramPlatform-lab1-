using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace NTracer.Tracer
{
    public class ThreadTracer
    {
        public Stack<MethodTracer> MethodTracers { get; }
        public List<MethodInformation> MethodsInformation { get; private set; }
        public int ThreadId { get; }

        public ThreadTracer()
        {
            this.MethodTracers = new Stack<MethodTracer>();
            this.ThreadId = Thread.CurrentThread.ManagedThreadId;
            this.MethodsInformation = new List<MethodInformation>();
        }

        public void StartTrace()
        {
            var stack = new StackTrace();
            var methodTracer = new MethodTracer();
            this.MethodTracers.Push(methodTracer);
            /*var stack = new StackTrace();
            var methodTracer = new MethodTracer();
            this.MethodTracers.Push(methodTracer);
            this.Information.InvokedMethods.Add(stack.GetFrame(1).GetMethod().Name);
            this.Information.InvokedMethods.Add(stack.GetFrame(2).GetMethod().Name);
            var frame = stack.GetFrames();
            methodTracer.StartTrace();*/
            methodTracer.StartTrace();
        }

        public void StopTrace()
        {
            var methodTracer = this.MethodTracers.Pop();
            methodTracer.StopTrace();
            StackFrame frame = new StackFrame(3); /// ???
            var method = frame.GetMethod();
            var methodInf = new MethodInformation(method.DeclaringType.ToString(), method.Name,
                methodTracer.GetElapsedTime(), null/*this.MethodsInformation*/);
            SetChildMethods(methodInf);
            /*this.MethodsInformation.Add(methodInf);
            var methodInf = this.MethodTracers.Pop().StopTrace();
            var stack = new StackTrace();
            var methodTracer = new MethodTracer();
            this.Information.InvokedMethods.Add(stack.GetFrame(1).GetMethod().Name);
            this.Information.MethodsInf.Add(methodInf);*/
        }

        public ThreadInformation GetThreadResult()
        {
            return new ThreadInformation(this.MethodsInformation, GetTotalMethodsTime());
        }

        private void SetChildMethods(MethodInformation methodParent)
        {
            StackFrame frame = new StackFrame(2); // ??
            var frames = new StackTrace().GetFrames();
            var method = frame.GetMethod();
            if (method.Name == "StopTrace")
            {
                this.MethodsInformation.Add(methodParent);
            }
            else
            {
                var methodsInf = new List<MethodInformation> { methodParent };
                this.MethodsInformation = methodsInf;
            }
        }

        private TimeSpan GetTotalMethodsTime()
        {
            var time = new TimeSpan();
            /*foreach (var methodInf in this.Information.MethodsInf)
            {
                time = time.Add(methodInf.ElapsedTime);
            }*/

            return time;
        }
    }
}

