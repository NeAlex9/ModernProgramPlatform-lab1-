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
    public class Foo
    {
        private Boo _boo;
        private ITracer _tracer;

        internal Foo(ITracer tracer)
        {
            _tracer = tracer;
            _boo = new Boo(_tracer);
        }

        public void MyMethod()
        {
            _tracer.StartTrace();
           new Thread(new ThreadStart(_boo.InnerMethod)).Start();
            _tracer.StopTrace();
        }
    }

    public class Boo
    {
        private ITracer _tracer;

        internal Boo(ITracer tracer)
        {
            _tracer = tracer;
        }

        public void InnerMethod()
        {
            _tracer.StartTrace();
            _tracer.StopTrace();
        }
    }

 /*   public class Bar
    {
        private mmm _mmm;
        private ITracer _tracer;

        internal Bar(ITracer tracer)
        {
            _mmm = new mmm(tracer);
            _tracer = tracer;
        }

        public void InnerMethod()
        {
            _tracer.StartTrace();
            new Thread(new ThreadStart(_mmm.InnerMethod));
            _tracer.StopTrace();
        }
    }

    public class mmm
    {
        private ITracer _tracer;

        internal mmm(ITracer tracer)
        {
            _tracer = tracer;
        }

        public void InnerMethod()
        {
            _tracer.StartTrace();
            _tracer.StopTrace();
        }
    }*/
}
