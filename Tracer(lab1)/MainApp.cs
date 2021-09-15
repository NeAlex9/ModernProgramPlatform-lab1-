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
            var first = new First(tracer);
            first.FirstM();
            /*boo.InnerMethod();
            foo.MyMethod();*/ 
            Console.ReadLine();
        }
    }
}
