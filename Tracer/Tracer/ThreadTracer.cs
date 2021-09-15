using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace NTracer.Tracer
{
    public class ThreadTracer
    {
        public Stack<MethodTracer> MethodTracers { get; }

        public TreeMaker TreeMaker { get; }

        public int ThreadId { get; }

        public ThreadTracer()
        {
            this.MethodTracers = new Stack<MethodTracer>();
            this.ThreadId = Thread.CurrentThread.ManagedThreadId;
            this.TreeMaker = new TreeMaker();
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
            StackFrame frame = new StackFrame(2);
            var method = frame.GetMethod();
            var methodInf = new MethodInformation(method.DeclaringType.ToString(), method.Name,
                methodTracer.GetElapsedTime(), new List<MethodInformation>());
            this.TreeMaker.MethodNodes.Push(new MethodNode(this.MethodTracers.Count + 1, methodInf));

            /*SetChildMethods(methodInf);
            this.MethodsInformation.Add(methodInf);
            var methodInf = this.MethodTracers.Pop().StopTrace();
            var stack = new StackTrace();
            var methodTracer = new MethodTracer();
            this.Information.InvokedMethods.Add(stack.GetFrame(1).GetMethod().Name);
            this.Information.MethodsInf.Add(methodInf);*/
        }

        public ThreadInformation GetThreadResult()
        {
            var methodsTree = this.TreeMaker.GetTreeByReverseSearch();
            return new ThreadInformation(methodsTree, GetTotalMethodsTime(methodsTree), Thread.CurrentThread.ManagedThreadId);
        }

        /*private void SetChildMethods(MethodInformation methodParent)
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
        }*/

        private TimeSpan GetTotalMethodsTime(List<MethodInformation> methodsTree)
        {
            var time = new TimeSpan();
            /*foreach (var methodInf in .MethodsInf)
            {
                time = time.Add(methodInf.ElapsedTime);
            }*/

            return time;
        }
    }
}

