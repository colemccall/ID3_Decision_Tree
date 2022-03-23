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
            TreeNode node = new TreeNode(); //have a method getMaxInfoGain A-0.82

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


        public string calculateInformationGain(int attribute)
        {
            //get the data type of this specifc attribute
            char dataType = TupleData.ElementAt(0).ElementAt(attribute).dataType;

            //initialize double
            double infoGain;

            //create counters for however many times each class is found
            /*--------Possible values are zero and one, needs to be changed if more output classes are required--------*/
            List<Data> freqData = new List<Data>();
            double total = 0;

            //Get frequency of each word
            for (int i = 0; i < TupleData.Count; i++)
            {
                //Get the attribute contents at current position and at the answer
                AttributeNode currentNode = TupleData.ElementAt(i).ElementAt(attribute);
                AttributeNode answerNode = TupleData.ElementAt(i).Last();

                //if working with words
                if (currentNode.dataType == 'S')
                {
                    bool containsData = false;
                    foreach (Data data in freqData)
                    {
                        if (data.doesContain(currentNode.word))
                        {
                            containsData = true;
                            data.count++;
                            total++;
                            break;
                        }
                    }

                    if (!containsData)
                    {
                        Data newData = new Data(currentNode.word, currentNode.classLabel);
                        freqData.Add(newData);
                        total++;
                    }

                }

                //if working with integers
                else if (currentNode.dataType == 'I')
                {
                    bool containsData = false;
                    foreach (Data data in freqData)
                    {
                        if (data.doesContain(currentNode.integer))
                        {
                            containsData = true;
                            data.count++;
                            total++;
                            break;
                        }
                    }

                    if (!containsData)
                    {
                        Data newData = new Data(currentNode.integer, currentNode.classLabel);
                        freqData.Add(newData);
                        total++;
                    }
                }

                //if working with continuous data
                else if (currentNode.dataType == 'C')
                {
                    bool containsData = false;
                    foreach (Data data in freqData)
                    {
                        if (currentNode.continuous == data.continous)
                        {
                            containsData = true;
                            data.count++;
                            total++;
                            break;
                        }
                    }

                    if (!containsData)
                    {
                        Data newData = new Data(currentNode.continuous, currentNode.classLabel);
                        freqData.Add(newData);
                        total++;
                    }

                }
            }

            //parallel lists for storing the probabilties of each attribute
            List<string> attributes = new List<string>();
            List<double> probabilties = new List<double>();
            List<string> possibilties = new List<string>();
            string output = "";

            if (dataType == 'S')
            {
                //calculate the probability of each class
                foreach (Data data in freqData)
                {
                    output += data.word + " is found " + data.count + "/" + total + " times\n";
                    double currentProb;
                    double currentCount = Convert.ToDouble(data.count);
                    currentProb = currentCount / total;

                    attributes.Add(data.attributeType);
                    probabilties.Add(currentProb);

                    if (!possibilties.Contains(data.attributeType))
                        possibilties.Add(data.attributeType);
                }
            }
            else if (dataType == 'I')
            {
                foreach (Data data in freqData)
                {
                    output += data.integer + " is found " + data.count + "/" + total + " times\n";
                    double currentProb;
                    double currentCount = Convert.ToDouble(data.count);
                    currentProb = currentCount / total;

                    attributes.Add(data.attributeType);
                    probabilties.Add(currentProb);

                    if (!possibilties.Contains(data.attributeType))
                        possibilties.Add(data.attributeType);
                }
            }

            else if (dataType == 'C')
            {
                foreach (Data data in freqData)
                {
                    output += data.continous + " is found " + data.count + "/" + total + " times\n";
                    double currentProb;
                    double currentCount = Convert.ToDouble(data.count);
                    currentProb = currentCount / total;

                    attributes.Add(data.attributeType);
                    probabilties.Add(currentProb);

                    if (!possibilties.Contains(data.attributeType))
                        possibilties.Add(data.attributeType);
                }
            }

            return getEntropy(attributes, possibilties, probabilties);
        }


        public string getEntropy(List<string> attributes, List<string> possibilities, List<double> probabilties)
        {
            string entropy = "";
            List<double> entropies = new List<double>();
            List<int> classesPerAttribute = new List<int>();

            //for each attribute on the list, find how many possible classes there are
            foreach (string possibility in possibilities)
            {
                //total entropy for one attribute
                int numberOfClasses = 0;
                for (int i = 0; i < attributes.Count; i++)
                {
                    //forreach attribute on the list calculate the enrtopy
                    if (attributes[i] == possibility)
                    {
                        numberOfClasses++;
                    }
                }
                classesPerAttribute.Add(numberOfClasses);
            }



            //for each attribute on the list, calculate the entropy
            for (int x = 0; x < possibilities.Count(); x++)
            {
                //total entropy for one attribute
                double attributeEntropy = 0;
                for (int i = 0; i < attributes.Count; i++)
                {
                    //forreach attribute on the list calculate the enrtopy
                    if (attributes[i] == possibilities[x])
                    {
                        double currentProb = probabilties[i];
                        double valueEntropy = currentProb * Math.Log(currentProb, classesPerAttribute[x]);
                        attributeEntropy += valueEntropy;
                    }
                }

                double flipNegativeEntropy = attributeEntropy - attributeEntropy - (attributeEntropy);
                entropies.Add(flipNegativeEntropy);
            }


            //print out all entropies
            for(int i = 0; i < entropies.Count; i++)
            { 
                entropy += "Entropy for " + possibilities[i] + " is " + entropies[i].ToString() + "\n";
                entropy += "Information Gain for " + possibilities[i] + " is " + (1 - entropies[i]) + "\n\n";
            }

            return entropy;
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
