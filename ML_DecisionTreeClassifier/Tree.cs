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





        public string calculateInformationGain(int attribute)
        {
            //get the data type of this specifc attribute
            char dataType = TupleData.ElementAt(0).ElementAt(attribute).dataType;

            //initialize double
            double infoGain;

            //create counters for however many times each class is found
            List<Data> freqData = new List<Data>();
            double total = 0;
            int numberOfClassesWithinAttribute = 0;

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
                            data.insert(answerNode.word);
                            total++;
                            break;
                        }
                    }

                    if (!containsData)
                    {
                        Data newData = new Data(currentNode.word, currentNode.classLabel, answerNode.word);
                        freqData.Add(newData);
                        total++;
                        numberOfClassesWithinAttribute++;
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
                            data.insert(answerNode.word);
                            total++;
                            break;
                        }
                    }

                    if (!containsData)
                    {
                        Data newData = new Data(currentNode.integer, currentNode.classLabel, answerNode.word);
                        freqData.Add(newData);
                        total++;
                        numberOfClassesWithinAttribute++;

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
                            data.insert(answerNode.word);
                            total++;
                            break;
                        }
                    }

                    if (!containsData)
                    {
                        Data newData = new Data(currentNode.continuous, currentNode.classLabel, answerNode.word);
                        freqData.Add(newData);
                        total++;
                        numberOfClassesWithinAttribute++;

                    }

                }
            }

            //parallel lists for storing the probabilties of each attribute
            List<string> attributes = new List<string>();
            List<double> probabilities = new List<double>();
            List<string> possibilities = new List<string>();

            //list for storing the counts based on split
            List<List<double>> splitProbabilities = new List<List<double>>();

            string output = "";

            if (dataType == 'S')
            {
                //calculate the probability of each class
                foreach (Data data in freqData)
                {
                    //calculate the probability of each class being randomly selected from the data set (Information Expected)
                    output += data.word + " is found " + data.count + "/" + total + " times\n";
                    double currentProb;
                    double currentCount = Convert.ToDouble(data.count);
                    currentProb = currentCount / total;

                    //add the class and probability of each attribute to parallel arrays
                    attributes.Add(data.attributeType);
                    probabilities.Add(currentProb);

                    //check to see how many classes are possible for each attribute
                    if (!possibilities.Contains(data.attributeType))
                        possibilities.Add(data.attributeType);


                    //calculate the probabilities of the answer based on the split on this attribute (Information Needed)

                    List<double> answerFrequency = new List<double>();
                    for (int x = 0; x < data.possibleAnswers.Count; x++)
                        answerFrequency.Add(0);

                    //add counts based on split
                    for (int x = 0; x < data.answers.Count(); x++)
                    {
                        for (int j = 0; j < data.possibleAnswers.Count(); j++)
                        {
                            if (data.answers[x] == data.possibleAnswers[j])
                                answerFrequency[j]++;
                        }
                    }


                    //divide by the amount of data in the dataset
                    for (int x = 0; x < answerFrequency.Count; x++)
                    {
                        answerFrequency[x] /= currentCount;
                    }

                    splitProbabilities.Add(answerFrequency);
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
                    probabilities.Add(currentProb);

                    if (!possibilities.Contains(data.attributeType))
                        possibilities.Add(data.attributeType);
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
                    probabilities.Add(currentProb);

                    if (!possibilities.Contains(data.attributeType))
                        possibilities.Add(data.attributeType);
                }
            }

            //get the frequency of each answer
            List<double> frequencies = new List<double>();
            List<string> possibleAnswers = new List<string>();
            for (int a = 0; a < TupleData.Count; a++)
            {
                //Get the attribute contents at the answer
                AttributeNode answerNode = TupleData.ElementAt(a).Last();
                if (!possibleAnswers.Contains(answerNode.word))
                {
                    possibleAnswers.Add(answerNode.word);
                    frequencies.Add(0);
                }
            }

            for (int a = 0; a < TupleData.Count; a++)
            {
                AttributeNode answerNode = TupleData.ElementAt(a).Last();
                for (int j = 0; j < possibleAnswers.Count; j++)
                {
                    if (answerNode.word == possibleAnswers[j])
                        frequencies[j]++;
                }
            }

            for (int a = 0; a < frequencies.Count; a++)
            {
                frequencies[a] = frequencies[a] / total;
            }

            /***************************************Prepare Final Calculations*********************************/

            //Calculate the expected information based on frequencies already found
            double finalExpectedInformation = getInformationExpected(frequencies);
            double finalInformationNeeded = getInformationNeeded(attributes, possibilities, probabilities, splitProbabilities);

            double finalInformationGain = finalExpectedInformation - finalInformationNeeded;


            //print out all information gains
            string finalOutput = "";
            finalOutput += "Expected information for " + possibilities[0] + " is " + finalExpectedInformation + "\n";
            finalOutput += "Needed information for " + possibilities[0] + " is " + finalInformationNeeded + "\n";
            finalOutput += "Information gain for " + possibilities[0] + " is " + finalInformationGain + "\n\n\n";



            return finalOutput;
        }





        /********************Information Expected Calculations**********************************/
        public double getInformationExpected(List<double> expectedInformation)
        {
            double flippedExpectedInformation = 0;
            for (int i = 0; i < expectedInformation.Count; i++)
            {
                double currentExpectedInformation = expectedInformation[i] * Math.Log2(expectedInformation[i]);
                flippedExpectedInformation += currentExpectedInformation;
            }
            double finalExpectedInformation = flippedExpectedInformation - flippedExpectedInformation - flippedExpectedInformation;
            return finalExpectedInformation;
        }


        /*****************Information Needed Calculations - much more difficult *********************/
        public double getInformationNeeded(List<string> attributes, List<string> possibilities, List<double> probabilties, List<List<double>> splitProbs)
        {
            string entropy = "";
            List<double> expected = new List<double>();
            List<double> needed = new List<double>();
            List<int> classesPerAttribute = new List<int>();

            //for each attribute on the list, find how many possible classes there are
            foreach (string possibility in possibilities)
            {
                int numberOfClasses = 0;
                for (int i = 0; i < attributes.Count; i++)
                {
                    if (attributes[i] == possibility)
                    {
                        numberOfClasses++;
                    }
                }
                classesPerAttribute.Add(numberOfClasses);
            }


            //for each attribute on the list, calculate the expected information
            for (int x = 0; x < possibilities.Count(); x++)
            {
                //expected information of one attribute
                double expectedInfo = 0;
                for (int i = 0; i < attributes.Count; i++)
                {
                    if (attributes[i] == possibilities[x])
                    {
                        expected.Add(probabilties[i]);
                    }
                }

            }


            //calculate final information needed
            double finalInformationNeeded = 0;
            foreach (List<double> probs in splitProbs)
            {
                double currentInformationNeeded = expected[0] * getInformationExpected(probs);
                finalInformationNeeded += currentInformationNeeded;
            }
            

            return finalInformationNeeded;
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
