using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML_DecisionTreeClassifier
{
    public class TreeNode
    {
        
        //Default Constructor
        public TreeNode()
        {
            Attribute = string.Empty;
            Type = string.Empty;
        }


        //Constructor
        public TreeNode(string attribute, string type)
        {
            Attribute = attribute;
            Type = type;
        }




        //Children List
        public List<TreeNode> Children = new List<TreeNode>();


        //Variables
        public string Attribute { get; set; }
        public string Type { get; set; }   
    }
}
