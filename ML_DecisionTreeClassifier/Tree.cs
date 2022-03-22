using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML_DecisionTreeClassifier
{
    public class Tree
    {
        public Tree(List<List<AttributeNode>> tuples, List<string> attributeList)
        {
            TupleData = tuples;    
            AttributeList = attributeList;
        }

        public void BuildTree()
        {
            //Create a node N
            TreeNode node = new TreeNode();

            //If tuples in D are all of the same class, return N as a leaf node labeled with the class C
            if(checkClasses())
            {
                node.IsLeaf = true;
                //node.type = C
                Root.AddChild(node);
            }

            //If the list of possible attributes is empty, return N as a leaf node with the majority class in D
            if(AttributeList == null)
            {
                node.IsLeaf = true;
                //node.type = majorityClass
                Root.AddChild(node);
            }

            //find the best splitting criterion


        }


        public void attributeGains(int numberOfAttributes)
        {
            //Create a list of information gains based on relevant data
            //List<double> gains = new List<double>();
            
            //For each attribute pair, find information gain
            for(int i = 0; i < numberOfAttributes - 1; i++)
            {
                //gains.Add(informationGain(i));
            }

            //return gains;
        }


        public string informationGain(int attribute)
        {
            //get the data type of this specifc attribute
            char dataType = TupleData.ElementAt(0).ElementAt(attribute).dataType;

            //initialize double
            double infoGain;

            //create counters for however many times each class is found
            /*--------Possible values are zero and one, needs to be changed if more output classes are required--------*/
            double freqZero = 0, freqOne = 0, totalFreq = 0, falseZero = 0, falseOne = 0;
            //List<Data> freqData = new List<Data>(); 


            for (int i = 0; i < TupleData.Count; i++)
            {
                //Get the attribute contents at current position and at the answer
                AttributeNode currentNode = TupleData.ElementAt(i).ElementAt(attribute);
                AttributeNode answerNode = TupleData.ElementAt(i).Last();


                //if the answer is a 0, then add 1 to both zero and total
                if (answerNode.word == "0" && currentNode.integer == 0)
                {
                    freqZero++;
                    totalFreq++;
                }

                //otherwise add to the total and 1 class
                else if (answerNode.word == "1" && currentNode.integer == 1)
                {
                    freqOne++;
                    totalFreq++;
                }

                else if (answerNode.word == "1" && currentNode.integer == 0)
                {
                    falseZero++;
                    totalFreq++;
                }
                else
                {
                    falseOne++;
                    totalFreq++;
                }

            }
                
            //calculate the probability of zero class and one class
            double probZero = (freqZero / totalFreq);
            double probOne = freqOne / totalFreq;

            string output = "\nProbability of 0 is " + probZero.ToString() + " ";
            output += "\nProbability of 1 " + probOne + " ";
            

            return output;
        }


        //take each tuple in Data and check if they all result in the same answer
        public bool checkClasses()
        {
            //first class is the result of the first training sample
            string firstClass = TupleData[0].Last().classLabel;

            
            for ( int i = 0; i< TupleData.Count; i++ )
            {
                //currentTuple is one line of training data
                List<AttributeNode> currentTuple = TupleData[i];

                //currentClass is the result (answer) class
                string currentClass = currentTuple.Last().classLabel;

                //check to see if the currentClass matches the first class
                if (currentClass != firstClass)
                    return false;
            }

            //otherwise
            return true;
        }



       


        public TreeNode Root { get; set; }
        public List<List<AttributeNode>> TupleData = new List<List<AttributeNode>>();
        public List<string> AttributeList { get; set; }



        //(1) create a node N;
        //(2) if tuples in D are all of the same class, C, then
        //(3) return N as a leaf node labeled with the class C;
        //(4) if attribute list is empty then
        //(5) return N as a leaf node labeled with the majority class in D; // majority voting
        //(6) apply Attribute selection method(D, attribute list) to find the “best” splitting criterion;
        //(7) label node N with splitting criterion;
        //(8) if splitting attribute is discrete-valued and
        //      multiway splits allowed then // not restricted to binary trees
        //(9) attribute list ← attribute list − splitting attribute; // remove splitting attribute
        //(10) for each outcome j of splitting criterion      
        // partition the tuples and grow subtrees for each partition
        //(11) let Dj be the set of data tuples in D satisfying outcome j; // a partition      
        //(12) if Dj is empty then attach a leaf labeled with the majority class in D to node N;
        //(14) else attach the node returned by Generate decision tree(Dj
        //, attribute list) to node N;
        //  endfor
        //(15) return N;
    }
}
