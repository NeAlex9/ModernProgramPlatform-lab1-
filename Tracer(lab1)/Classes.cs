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
    public class First
    {
        private ITracer _tracer;
        private Second _second;

        internal First(ITracer tracer)
        {
            _tracer = tracer;
            _second = new Second(_tracer);
        }

        public void FirstM()
        {
            _tracer.StartTrace();
            _second.SecondM();
            _tracer.StopTrace();    
        }
    }

    public class Second
    {
        private ITracer _tracer;
        private Third _third;
        private Fourth _fourth;

        public Second(ITracer tracer)
        {
            _tracer = tracer;
            _third = new Third(_tracer);
            _fourth = new Fourth(_tracer);
        }

        public void SecondM()
        {
            _tracer.StartTrace();
            _third.ThirdM();
            _fourth.FourthM();
            _tracer.StopTrace();
        }
    }

    public class Third
    {
        private ITracer _tracer;

        internal Third(ITracer tracer)
        {
            _tracer = tracer;
        }

        public void ThirdM()
        {
            _tracer.StartTrace();
            _tracer.StopTrace();
        }
    }

    public class Fourth
    {
        private ITracer _tracer;

        internal Fourth(ITracer tracer)
        {
            _tracer = tracer;
        }

        public void FourthM()
        {
            _tracer.StartTrace();
            _tracer.StopTrace();
        }
    }
}
