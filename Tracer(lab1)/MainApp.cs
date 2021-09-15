using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NTracer;
using NTracer.Tracer;

namespace Tracer_lab1_
{
    class MainApp
    {
        public static void Main()
        {
            Tracer tracer = new Tracer();
            var second = new Second(tracer);
            var t1 = new Thread(new ThreadStart(second.SecondM));
            t1.Start();
            var first = new First(tracer);
            first.FirstM();
            Thread.Sleep(3000);
            var res = tracer.GetTraceResult();
            Console.ReadLine();
        }
    }
}
