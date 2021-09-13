using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTracer.Tracer
{
    public class TraceResult
    {
        public ImmutableList<ThreadNode> Threads { get; set; }
    }

    public class ThreadNode
    {
        public int Id { get; set; }

        public TimeSpan ThreadTime { get; set; }

        public ImmutableList<MethodNode> ChildMembers { get; set; }
    }

    public class MethodNode : MethodInformation
    {
        public ImmutableList<MethodNode> ChildMembers { get; set; }
    }
}
