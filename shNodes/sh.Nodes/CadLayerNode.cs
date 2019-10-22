using System;
using System.Collections.Generic;
using System.Text;

namespace sh.Nodes
{
    public class CadLayerNode:Node
    {
        public string ColorIndex { get { return GetString(); } }

        public string LineWeight { get { return GetString(); } }
    }
}
