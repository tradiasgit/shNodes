using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sh.Nodes
{
    public class ParamaterNode : Node
    {
        public string GetStringValue()
        {
            return GetString("Value");
        }

        public KeyValuePair<string, string>? GetKVPairValue()
        {
            var value = GetElementValue(Data, "Value");
            if (value != null && value.IsBsonDocument)
            {
                var k = GetString(value.AsBsonDocument, "Key", false);
                var v = GetString(value.AsBsonDocument, "Value", false);
                return new KeyValuePair<string, string>(k,v);
            }
            return null;
        }
    }
}
