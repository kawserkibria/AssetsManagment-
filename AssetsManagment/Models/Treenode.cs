using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AssetsManagment.Models
{
    public class Treenode
    {
        public string id;
        public string text;
        public string icon;
        public string type;
       
        public List<Treenode> children;

        public static Treenode NewNode(string id, string typeNode = "first")
        {
            return new Treenode()
            {
                id = id,
                text = string.Format(id),
                type = typeNode,
                children = new List<Treenode>()
            };
        }
    }
    
}