using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace sh.Nodes
{
    public class TreeNode:Node
    {
        public string Tooltip { get { return GetString(); } }

        public ICollection<Node> ChildNodes { get; private set; }
        public ICollection<Node> ActionNodes { get; private set; }

        public override void LoadChildren()
        {
            base.LoadChildren();
            ChildNodes = GetChildNodes("ChildNodes");
            ActionNodes = GetChildNodes("ActionNodes"); 
        }
    }
}
