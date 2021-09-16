using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NTracer.Tracer;

namespace NTracer.Serialization
{
    public class JsonSerialization : ISerializable
    {
        public string Serialize(TraceResult traceResult)
        {
            JArray threadJArray = new JArray();
            foreach (var thread in traceResult.Threads)
            {
                JObject threadJObject = GetThreadJObject(thread);
                threadJArray.Add(threadJObject);
            }
            JObject resultJObject = new JObject
            {
                {"threads", threadJArray }
            };
            StringWriter stringWriter = new StringWriter();
            using (var jsonWriter = new JsonTextWriter(stringWriter))
            {
                jsonWriter.Formatting = Formatting.Indented;
                resultJObject.WriteTo(jsonWriter);
            }
            return stringWriter.ToString();
        }

        private JObject GetPartSerializedMethod(MethodInformation methodInfo)
        {
            return new JObject
            {
                {"name", methodInfo.MethodName },
                {"class", methodInfo.ClassName },
                {"time", methodInfo.ElapsedTime.TotalMilliseconds.ToString() + "ms" },
            };
        }

        private JObject GetSerializedMethod(MethodInformation methodInfo)
        {
            JObject methodJObject = GetPartSerializedMethod(methodInfo);
            JArray methodsJArray = new JArray();
            foreach (var method in methodInfo.ChildMethods)
            {
                JObject childMethodJObject = GetSerializedMethod(method);
                methodsJArray.Add(childMethodJObject);
            }

            methodJObject.Add("methods", methodsJArray);
            return methodJObject;
        }

        private JObject GetThreadJObject(ThreadInformation threadInfo)
        {
            JArray methodJArray = new JArray();
            foreach (var method in threadInfo.MethodsInf)
            {
                JObject methodJObject = GetSerializedMethod(method);

                methodJArray.Add(methodJObject);
            }
            return new JObject
            {
                {"id", threadInfo.Id },
                {"time", threadInfo.TotalMethodsTime.TotalMilliseconds.ToString() + "ms" },
                {"methods", methodJArray }
            };
        }
    }
}

