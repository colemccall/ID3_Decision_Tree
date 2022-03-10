using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML_DecisionTreeClassifier
{
    public class Tree
    {
        public Tree()
        {

        }


        public TreeNode Root = new TreeNode();
        public List<List<AttributeNode>> Data = new List<List<AttributeNode>>();
    }
}
