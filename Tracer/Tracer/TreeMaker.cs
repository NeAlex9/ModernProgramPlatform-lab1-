using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTracer.Tracer
{
    public class TreeMaker
    {
        public List<MethodInformation> Root { get; }
        public Stack<MethodNode> MethodNodes { get; }

        public TreeMaker()
        {
            this.MethodNodes = new Stack<MethodNode>();
            this.Root = new List<MethodInformation>();
        }

        public List<MethodInformation> GetTreeByReverseSearch()
        {
            while (this.MethodNodes.Count > 0)
            {
                var nodeInformation = this.MethodNodes.Pop();
                var currentMethodNode = GetCorrectMethodNode(nodeInformation.DeepLevel);
                currentMethodNode.Insert(0, nodeInformation.MethodInformation);
            }

            return this.Root;
        }

        private List<MethodInformation> GetCorrectMethodNode(int deepLevel)
        {
            List<MethodInformation> temp = Root;
            for (int i = 0; i < deepLevel - 1; i++)
            {
                temp = temp[0].ChildMethods;
            }

            return temp;
        }
    }

    public class MethodNode
    {
        public int DeepLevel { get; }
        public MethodInformation MethodInformation { get; }

        public MethodNode(int deepLevel, MethodInformation methodInformation)
        {
            this.MethodInformation = methodInformation;
            this.DeepLevel = deepLevel;
        }
    }
}
