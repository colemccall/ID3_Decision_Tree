using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML_DecisionTreeClassifier
{
    public class TreeNode
    {
        public TreeNode()
        {

        }

        



        //Method for adding a child to the list of children
        public void AddChild(TreeNode child)
        {
            Children.Add(child);
        }




        //Children List
        public List<TreeNode> Children = new List<TreeNode>();

        
        //variable for determining if node is a leaf or had children
        public bool IsLeaf { get; set; }

        public double informationGain { get; set; }

        public string type { get; set; }
    }
}
