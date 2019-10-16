using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sh.Nodes
{
    public class ActionNode:Node
    {

        public string Class { get { return GetString(); } }

        public string Method { get { return GetString(); } }

        public IEnumerable<Node> Paramaters { get { return GetChildNodes("Paramaters"); } }

    }
}
