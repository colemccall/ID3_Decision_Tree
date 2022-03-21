using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML_DecisionTreeClassifier
{
    public class Tree
    {
        public Tree(List<List<AttributeNode>> tuples)
        {
            Data = tuples;            
        }

        public void BuildTree()
        {
            
        }



       


        public TreeNode Root { get; set; }
        public List<List<AttributeNode>> Data = new List<List<AttributeNode>>();
    }
}
