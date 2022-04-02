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
            attributeValue = "none";
        }

        public TreeNode()
        {
            informationExpected = 0;
            informationNeeded = 0;
            informationGain = 0;
            attributeValue = "none";
        }

        public TreeNode(string attribute, string finalAnswer, string attributeValue)
        {
            informationExpected = 0;
            informationNeeded = 0;
            informationGain = 0;
            this.attribute = attribute;
            this.finalAnswer = finalAnswer;
            this.attributeValue = attributeValue;
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
            Children.Add(child);
        }


     



        //Children List
        public List<TreeNode> Children = new List<TreeNode>();


        //variable for determining if node is a leaf or had children
        public bool IsLeaf { get; set; }

        //variables created to store information needed, expected, and gain
        public double informationExpected { get; set; }
        public double informationNeeded { get; set; }
        public double informationGain { get; set; }



        //variables created to store the attribute and the value (class)
        public List<string> values { get; set; }
        public string attribute { get; set; }

        public string finalAnswer { get; set; }

        public string attributeValue { get; set; }  
    }
}
