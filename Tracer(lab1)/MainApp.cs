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
            var foo = new Foo(tracer);
            var boo = new Boo(tracer);
            foo.MyMethod();
            boo.InnerMethod();
            /*boo.InnerMethod();
            foo.MyMethod();*/
            tracer.ThreadTracers[0].Information.MethodsInf = tracer.ThreadTracers[0].Information.MethodsInf.OrderBy(elem => elem.SortedId).ToList();
             Console.ReadLine();
        }
    }
}
