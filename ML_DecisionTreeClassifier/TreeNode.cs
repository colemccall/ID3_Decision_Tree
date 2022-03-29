using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML_DecisionTreeClassifier
{
    public class TreeNode
    {
        public TreeNode(double expected, double needed, double gain, string attribute, List<string> values)
        {
            informationExpected = expected;
            informationNeeded = needed;
            informationGain = gain;
            this.attribute = attribute;
            this.values = values;
        }

        public TreeNode()
        {
            informationExpected = 0;
            informationNeeded = 0;
            informationGain = 0;
        }

        //Method for outputting a node
        public string View()
        {
            string finalOutput = "";
            finalOutput += "Expected information for " + attribute + " is " + Math.Round(informationExpected, 3) + "\n";
            finalOutput += "Needed information for " + attribute + " is " + Math.Round(informationNeeded, 3) + "\n";
            finalOutput += "Information gain for " + attribute + " is " + Math.Round(informationGain, 3) + "\n\n\n";

            return finalOutput;

        }




        //Method for adding a child to the list of children
        public void AddChild(TreeNode child)
        {
            if (Children.Count < 2)
                Children.Add(child);
            else
                Children[1].AddChild(child);
        }


        public void AddNode(TreeNode node)
        {
            OtherNodes.Add(node);   
        }



        //Children List
        public List<TreeNode> Children = new List<TreeNode>();

        //Fake children list for testing
        public List<TreeNode> OtherNodes = new List<TreeNode> ();


        //variable for determining if node is a leaf or had children
        public bool IsLeaf { get; set; }

        //variables created to store information needed, expected, and gain
        public double informationExpected { get; set; }
        public double informationNeeded { get; set; }
        public double informationGain { get; set; }



        //variables created to store the attribute and the value (class)
        public List<string> values { get; set; }
        public string attribute { get; set; }
    }
}
