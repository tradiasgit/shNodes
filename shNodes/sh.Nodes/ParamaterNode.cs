using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace sh.Nodes
{
    public class ParamaterNode : Node
    {
        public string Value { get { return GetString(); } }

        public string ParamaterType { get { return GetString(); } }

        public Node ValueNode
        {
            get
            {
                BsonElement ele;
                if (Data.TryGetElement("ValueNode", out ele)&&ele.Value.IsBsonDocument)
                {
                    var doc = ele.Value.AsBsonDocument;
                    return GetNode(doc);
                }
                return null;
            }
        }
    }
}
