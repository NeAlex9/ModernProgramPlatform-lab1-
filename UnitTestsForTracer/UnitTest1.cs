using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NTracer.Tracer;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;

namespace UnitTestsForTracer
{
    [TestFixture]
    public class UnitTest1
    {
        private int _latencyInMilliseconds = 1000;
        private Tracer _tracer;
        private delegate void MethodInvokerDelegate();

        [TestCase(5, 5)]
        //[TestCase(6, 6, Method_Without_SubMethods)]
        public void Check_Number_Threads(int threadsCount, int result)
        {
            Setup(threadsCount);
            var traceResult = _tracer.GetTraceResult();
            Assert.That(traceResult.Threads.Count, Is.EqualTo(result));
        }

        //public void Check_Number_Methods(int )

        private void Setup(int threadsCount)
        {
            _tracer = new Tracer();
            var threads = new List<Thread>();
            Run_Number_Of_Threads(threads, threadsCount);
        }

        private void Run_Number_Of_Threads(List<Thread> threads, int threadsCount)
        {
            for (var i = 0; i < threadsCount; i++)
            {
                threads.Add(new Thread(new ParameterizedThreadStart(Method_With_One_SubMethods)));
            }

            foreach (var thread in threads)
            {
                thread.Start(this._latencyInMilliseconds);
            }

            foreach (var thread in threads)
            {
                thread.Join();
            }
        }

        private void Method_Without_SubMethods(object latency)
        {
            _tracer.StartTrace();
            Thread.Sleep((int)latency);
            _tracer.StopTrace();
        }

        private void Method_With_One_SubMethods(object latency)
        {
            _tracer.StartTrace();
            Method_Without_SubMethods(latency);
            _tracer.StopTrace();
        }
    }
}
