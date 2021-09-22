using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NTracer;
using NTracer.Serialization;
using NTracer.Tracer;
using NTracer.Writer;
using ISerializable = NTracer.Serialization.ISerializable;

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
            Thread.Sleep(2000);
            var res = tracer.GetTraceResult();
            ISerializable serialization= new JsonSerialization();
            var json = serialization.Serialize(res);
            IWriter textWriter = new ConsoleWriter();
            textWriter.Write(json);
            Console.ReadLine();
        }
    }
}
