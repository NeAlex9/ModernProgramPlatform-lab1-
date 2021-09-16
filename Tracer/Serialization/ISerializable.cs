using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NTracer.Tracer;

namespace NTracer.Serialization
{
    public interface ISerializable
    {
        string Serialize(TraceResult traceResult);
    }
}
