using System;
using System.Collections.Generic;
using System.Text;

namespace sh.Nodes
{
    public class CadEntityFilterNode:Node
    {
        public IEnumerable<Node> DataNodes { get; }

        public IEnumerable<Node> LayerNodes { get; }
    }
}
