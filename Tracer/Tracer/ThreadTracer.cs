using System;
using System.CodeDom;
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
        }

        public ThreadInformation GetThreadResult()
        {
            var methodsTree = this.TreeMaker.GetTreeByReverseSearch();
            return new ThreadInformation(methodsTree, GetTotalMethodsTime(methodsTree), this.ThreadId);
        }

        private TimeSpan GetTotalMethodsTime(List<MethodInformation> methodNodes)
        {
            var time = new TimeSpan();
            foreach (var methodNode in methodNodes)
            {
                time = time.Add(methodNode.ElapsedTime);
                time = time.Add(GetTotalMethodsTime(methodNode.ChildMethods));
            }

            return time;
        }
    }
}

