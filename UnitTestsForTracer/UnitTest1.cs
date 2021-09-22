using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NTracer.Tracer;
using NUnit.Framework;
using Tracer_lab1_;
using Assert = NUnit.Framework.Assert;

namespace UnitTestsForTracer
{
    [TestFixture]
    public class UnitTest1
    {
        private Tracer _tracer;

        [Test]
        public void Check_NumberThreads_WithThreads()
        {
            Setup();
            var third = new Third(_tracer);
            third.ThirdM();
            var t1 = new Thread(new ThreadStart(third.ThirdM));
            t1.Start();
            t1.Join();
            var traceResult = _tracer.GetTraceResult();
            Assert.That(traceResult.Threads.Count, Is.EqualTo(2));
        }

        [TestCase(1000)]
        [TestCase(2000)]
        public void Check_Time(int time)
        {
            Setup();
            int latency = 100;
            var fifth = new Fifth(_tracer);
            fifth.FifthM(time);
            var traceResult = _tracer.GetTraceResult();
            Assert.That(traceResult.Threads[0].MethodsInf[0].ElapsedTime.TotalMilliseconds, Is.AtMost(time + latency));
        }

        [Test]
        public void Check_NumberMethods_WithMethods()
        {
            Setup();
            var third = new Third(_tracer);
            third.ThirdM();
            var t1 = new Thread(new ThreadStart(third.ThirdM));
            t1.Start();
            t1.Join();
            var traceResult = _tracer.GetTraceResult();
            Assert.That(CountMethodsInTraceResult(traceResult), Is.EqualTo(2));
        }

        [Test]
        public void Check_NumberMethods_WithNestedMethodsAndThread()
        {
            Setup();
            var second = new Second(_tracer);
            second.SecondM();
            Thread.Sleep(3000);
            var traceResult = _tracer.GetTraceResult();
            Assert.That(CountMethodsInTraceResult(traceResult), Is.EqualTo(4));
        }

        [Test]
        public void Check_NumberMethods_WithComplexStructure()
        {
            Setup();
            var first = new First(_tracer);
            first.FirstM();
            Thread.Sleep(3000);
            var traceResult = _tracer.GetTraceResult();
            Assert.That(CountMethodsInTraceResult(traceResult), Is.EqualTo(6));
        }

        [Test]
        public void Check_ThreadId()
        {
            Setup();
            var third = new Third(_tracer);
            third.ThirdM();
            var t1 = new Thread(new ThreadStart(third.ThirdM));
            t1.Start();
            t1.Join();
            var traceResult = _tracer.GetTraceResult();
            Assert.That(new int[Thread.CurrentThread.ManagedThreadId, t1.ManagedThreadId], Is.EqualTo(new int[traceResult.Threads[0].Id, traceResult.Threads[1].Id]));
        }

        [Test]
        public void Check_ThreadMethodsNames()
        {
            Setup();
            var second = new Second(_tracer);
            var third = new Third(_tracer);
            second.SecondM();
            var t1 = new Thread(new ThreadStart(third.ThirdM));
            t1.Start();
            t1.Join();
            var threads = _tracer.GetTraceResult().Threads;
            var actualResult = new string[][]{ new string[] { "SecondM", "ThirdM", "FourthM" }, new string[] { "FifthM" } };
            var currentResult = new string[][] { new string[] { threads[0].MethodsInf[0].MethodName, threads[0].MethodsInf[0].ChildMethods[0].MethodName, threads[0].MethodsInf[0].ChildMethods[1].MethodName }, new string[] { threads[1].MethodsInf[0].MethodName } };
            Assert.That(currentResult, Is.EqualTo(actualResult));
        }

        [Test]
        public void Check_ThreadClassNames()
        {
            Setup();
            var second = new Second(_tracer);
            var third = new Third(_tracer);
            second.SecondM();
            var t1 = new Thread(new ThreadStart(third.ThirdM));
            t1.Start();
            t1.Join();
            var threads = _tracer.GetTraceResult().Threads;
            var actualResult = new string[][] { new string[] { "Second", "Third", "Fourth" }, new string[] { "Fifth" } };
            var currentResult = new string[][] { new string[] { threads[0].MethodsInf[0].ClassName, threads[0].MethodsInf[0].ChildMethods[0].ClassName, threads[0].MethodsInf[0].ChildMethods[1].ClassName }, new string[] { threads[1].MethodsInf[0].ClassName } };
            Assert.That(currentResult, Is.EqualTo(actualResult));
        }

        private void CountMethodsInThread(IReadOnlyCollection<MethodInformation> result, ref int count)
        {
            if (result.Count > 0)
            {
                foreach (var method in result)
                {
                    count += 1;
                    CountMethodsInThread(method.ChildMethods, ref count);
                }
            }
        }

        private int CountMethodsInTraceResult(TraceResult res)
        {
            int count = 0;
            foreach (var thread in res.Threads)
            {
                CountMethodsInThread(thread.MethodsInf, ref count);
            }

            return count;
        }

        private void Setup()
        {
            _tracer = new Tracer();
        }
    }
}
