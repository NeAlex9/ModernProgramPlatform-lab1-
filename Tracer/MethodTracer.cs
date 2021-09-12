using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTracer
{
    public class MethodTracer
    {
        private static int MethodId = 0;

        private MethodInformation MethodInf { get; set; }

        private DateTime StartTime { get; set; }

        public void StartTrace()
        {
            this.MethodInf = new MethodInformation(MethodTracer.MethodId);
            MethodTracer.MethodId++;
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

    public class MethodInformation
    {
        public MethodInformation(int id)
        {
            this.MethodeId = id;
        }

        public string ClassName { get; set; }
        public string MethodName { get; set; }
        public TimeSpan ElapsedTime { get;  set; }
        public int MethodeId { get; }
    }
}
