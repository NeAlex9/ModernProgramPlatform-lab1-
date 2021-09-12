using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NTracer
{
    public class ThreadTracer
    {
        public ThreadTracer()
        {
            this.Infornmation = new ThreadInformation();
            this.MethodTracers = new Stack<MethodTracer>();
        }

        public void StartTrace()
        {
            var stack = new StackTrace();
            var methodTracer = new MethodTracer();
            this.MethodTracers.Push(methodTracer);
            this.Infornmation.InvokedMethods.Add(stack.GetFrame(1).GetMethod().Name);
            this.Infornmation.InvokedMethods.Add(stack.GetFrame(2).GetMethod().Name);
            var frame = stack.GetFrames();
            methodTracer.StartTrace();
        }

        public void StopTrace()
        {
            var methodInf = this.MethodTracers.Pop().StopTrace();
            var stack = new StackTrace();
            var methodTracer = new MethodTracer();
            this.Infornmation.InvokedMethods.Add(stack.GetFrame(1).GetMethod().Name);
            this.Infornmation.MethodsInf.Add(methodInf);
        }

        public Stack<MethodTracer> MethodTracers { get; set; }
        public ThreadInformation Infornmation { get; set; }
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

        public int Id { get; private set; }

        public List<MethodInformation> MethodsInf { get; set; }
    }
}
