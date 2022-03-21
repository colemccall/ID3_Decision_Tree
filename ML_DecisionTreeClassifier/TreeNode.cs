using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML_DecisionTreeClassifier
{
    public class TreeNode
    {
        public TreeNode(AttributeNode child)
        {
            node = child;
        }

        



        //Method for adding a child to the list of children
        public void AddChild(AttributeNode child)
        {
            Children.Add(new TreeNode(child));
        }




        //Children List
        public List<TreeNode> Children = new List<TreeNode>();

        
        //variable for determining if node is a leaf or had children
        public bool IsLeaf { get; set; }

        //Object created to help store training tuples
        public AttributeNode node { get; set; }

        public double informationGain { get; set; }
    }
}
