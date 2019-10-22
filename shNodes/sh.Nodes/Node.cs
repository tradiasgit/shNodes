using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace sh.Nodes
{
    public abstract class Node
    {
        #region DbContext
        private static IMongoDatabase GetDatabase()
        {
            var client = new MongoClient("mongodb+srv://hh:skingsking@s-tzqg1.mongodb.net/test?retryWrites=true&w=majority");
            var db = client.GetDatabase("dev");
            return db;
        }
        private static BsonDocument FindOne(string oid, string collectionName = "Nodes")
        {
            if (!_cache.ContainsKey(oid))
            {
                var db = GetDatabase();
                var coll = db.GetCollection<BsonDocument>(collectionName);
                var filter = Builders<BsonDocument>.Filter.Eq("_id", new ObjectId(oid));
                var one= coll.Find(filter).FirstOrDefault();
                _cache.Add(oid, one);
            }
            return _cache[oid];
        }
        #endregion

        private static Dictionary<string, BsonDocument> _cache = new Dictionary<string, BsonDocument>();

        public static Node GetNode(string id)
        {
            var doc = FindOne(id);
            if (doc != null) return GetNode(doc);
            return null;
        }
        internal static Node GetNode(BsonDocument doc)
        {
            var nslsit = new List<string>()
            {
                "sh.Nodes",
            };
            var typestring = GetString(doc, "Type");
            if (string.IsNullOrWhiteSpace(typestring)) typestring = "TreeNode";
            if (!string.IsNullOrWhiteSpace(typestring))
            {
                foreach (var ns in nslsit)
                {
                    var type = System.Type.GetType($"{ns}.{typestring}");
                    if (type != null)
                    {
                        var node = Activator.CreateInstance(type) as Node;
                        node.Load(doc);
                        return node;
                    }
                }
            }
            return null;
        }

        protected static BsonValue GetElementValue(BsonDocument doc, string name, bool readBase = true)
        {
            BsonElement ele;
            if (doc.TryGetElement(name, out ele))
            {
                return ele.Value;
            }
            BsonElement baseEle;
            if (readBase && doc.TryGetElement("BasedOn", out baseEle))
            {
                var baseDoc = FindOne(baseEle.Value.AsString);
                if (baseDoc != null) return GetElementValue(baseDoc, name);
            }
            return null;
        }


        protected static string GetString(BsonDocument doc, string name, bool readBase = true)
        {
            var value = GetElementValue(doc, name, readBase);
            if (value != null && value.IsString) return value.AsString;
            return null;
        }

        #region 构造和字段


        protected void Load(BsonDocument doc)
        {
            Data = doc;
            Id = GetString("Id", false);
            BasedOn = GetString("BasedOn", false);
            LoadBaseNode();
            LoadChildren();
        }

        protected BsonDocument Data;
        protected Node BaseNode;

        public string Id { get; set; }
        public string BasedOn { get; private set; }

        public virtual void LoadBaseNode()
        {
            if (!string.IsNullOrWhiteSpace(BasedOn))
            {
                BaseNode = GetNode(BasedOn);
            }
        }

        #endregion

        protected bool HasBaseNode
        {
            get
            {
                return BaseNode != null;
            }
        }


        public string GetString([CallerMemberName]string name = null, bool readBase = true)
        {
            BsonValue value;
            if (Data != null && Data.TryGetValue(name, out value)) return value?.ToString();
            else if (readBase && HasBaseNode) return BaseNode.GetString(name, readBase);
            else return null;
        }






        public string Name { get { return GetString(); } }

        public string Type { get { return GetString(); } }




        protected ICollection<Node> GetChildNodes(string childrenFieldName)
        {
            var result = new List<Node>();
            if (HasBaseNode)
            {
                var baseChildNodes = BaseNode.GetChildNodes(childrenFieldName);
                result.AddRange(baseChildNodes);
            }
            var childNodes = GetChildNodes(childrenFieldName, Data);
            result.AddRange(childNodes);
            return result;
        }

        protected static ICollection<Node> GetChildNodes(string childrenFieldName, BsonDocument doc)
        {
            BsonElement childrenElement;
            var result = new List<Node>();
            if (doc != null && doc.TryGetElement(childrenFieldName, out childrenElement) && childrenElement.Value.IsBsonArray)
            {
                var nodes = childrenElement.Value.AsBsonArray;
                foreach (var n in nodes)
                {

                    Node node = null;
                    if (n.IsBsonDocument)
                    {
                        node = GetNode(n.AsBsonDocument);
                    }
                    else if (n.IsString)
                    {
                        node = GetNode(n.AsString);
                    }
                    if (node != null)
                    {
                        result.Add(node);
                    }
                }
            }
            return result;
        }


        public virtual void LoadChildren() { }


    }
}
