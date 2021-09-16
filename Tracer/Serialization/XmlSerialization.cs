using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using NTracer.Tracer;

namespace NTracer.Serialization
{
    public class XmlSerialization : ISerializable
    {
        public string Serialize(TraceResult traceResult)
        {
            var threadsInfo = traceResult.Threads;
            XDocument xDocument = new XDocument(new XElement("root"));

            foreach (ThreadInformation thread in threadsInfo)
            {
                XElement threadXElement = GetThread(thread);
                xDocument.Root.Add(threadXElement);
            }

            StringWriter stringWriter = new StringWriter();
            using (XmlTextWriter xmlWriter = new XmlTextWriter(stringWriter))
            {
                xmlWriter.Formatting = Formatting.Indented;
                xDocument.WriteTo(xmlWriter);
            }
            return stringWriter.ToString();
        }

        private XElement GetThread(ThreadInformation threadInfo)
        {
            var threadXElement = new XElement(
                "thread",
                new XAttribute("id", threadInfo.Id),
                new XAttribute("time", threadInfo.TotalMethodsTime.TotalMilliseconds + "ms")
            );
            foreach (var method in threadInfo.MethodsInf)
            {
                var methodXElement = GetSerializedMethod(method);
                threadXElement.Add(methodXElement);
            }

            return threadXElement;
        }
        
        private XElement GetSerializedMethod(MethodInformation methodInfo)
        {
            var methodXElement = GetPartSerializedMethod(methodInfo);
            foreach (var method in methodInfo.ChildMethods)
            {
                var childMethod = GetSerializedMethod(method);
                methodXElement.Add(childMethod);
            }

            return methodXElement;
        }

        private XElement GetPartSerializedMethod(MethodInformation methodInfo)
        {
            return new XElement(
                "method",
                new XAttribute("name", methodInfo.MethodName),
                new XAttribute("class", methodInfo.ClassName),
                new XAttribute("time", methodInfo.ElapsedTime.TotalMilliseconds + "ms")
                );
        }
    }
}
