using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sh.Nodes
{
    public class ValueNode : Node
    {
        public string Value { get { return GetString(); } }
    }
}
