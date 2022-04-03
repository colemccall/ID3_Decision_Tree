using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML_DecisionTreeClassifier
{
    public class Tree
    {
        public Tree(List<List<AttributeNode>> tuples, List<string> attributeList, int numberOfClasses)
        {
            TupleData = tuples;
            AttributeList = attributeList;
            this.numberOfClasses = numberOfClasses;
        }

        //reclassify a specific attribute from continuous to nominal
        private List<List<AttributeNode>> removeContinuous(List<List<AttributeNode>> currentDataMatrix, double splitPoint, int attributeIndex)
        {
            //create a new empty data matrix
            List<List<AttributeNode>> newMatrix = new List<List<AttributeNode>>();

            //for every single node where the attribute is contininous, reclassify based on split type
            List<List<AttributeNode>> currentMatrix = new List<List<AttributeNode>>(currentDataMatrix);
            foreach (List<AttributeNode> line in currentDataMatrix)
            {
                //we want to keep the actual line in tact, so lets create a copy
                List<AttributeNode> currentLine = new List<AttributeNode>();
                foreach(AttributeNode attribute in line)
                {
                    if (attribute.converted == false)
                        currentLine.Add(new AttributeNode(attribute.classLabel, attribute.dataType, attribute.continuous));
                    else
                        currentLine.Add(new AttributeNode(attribute.classLabel, attribute.dataType, attribute.word));
                }


                //get the current value and check if it is less than or greater than the split point
                double currentValue = currentLine[attributeIndex].continuous;

                if (currentValue > splitPoint)
                {
                    string newValue = ">" + splitPoint.ToString();
                    currentLine[attributeIndex].dataType = 'S';
                    currentLine[attributeIndex].word = newValue;
                    currentLine[attributeIndex].converted = true;
                }
                else
                {
                    string newValue = "<=" + splitPoint.ToString();
                    currentLine[attributeIndex].dataType = 'S';
                    currentLine[attributeIndex].word = newValue;
                    currentLine[attributeIndex].converted = true;
                }

                newMatrix.Add(currentLine.ToList());

            }

            return newMatrix;

        }


        //reclassify all continuous attributes based on a list that contains which indexes are continiuous
        public void removeAllContinuous(List<int> continuousIndexes, List<List<double>> possibleSplitPoints)
        {
            //start out with the base data matrix
            List<List<AttributeNode>> updatedMatrix = new List<List<AttributeNode>>(TupleData);
            

            
            for(int i = 0; i < continuousIndexes.Count; i++)
            {
                //for each continuous attribute, create a list of the new possible matrixes
                List<List<List<AttributeNode>>> possibleNewMatrixes = new List<List<List<AttributeNode>>>();

                //each possible matrix will be created based on all the possible split points
                foreach (double currentSplitPoint in possibleSplitPoints[i])
                {
                    //AGHHHHHHHHHHHHHHHHHh - needs to create a new matrix, not just keep updating the current one
                    List<List<AttributeNode>> currentMatrix = new List<List<AttributeNode>>(updatedMatrix);
                    possibleNewMatrixes.Add(removeContinuous(currentMatrix.ToList(), currentSplitPoint, continuousIndexes[i]));
                }

                //once we have all of our possible new matrices, we need to calculate information gain for these matrices
                List<TreeNode> possibleNodes = new List<TreeNode>();    

                foreach (List<List<AttributeNode>> possibleMatrix in possibleNewMatrixes)
                {
                    //get the information gain of each possible matrix
                    possibleNodes.Add(calculateInformationGain(possibleMatrix, continuousIndexes[i]));
                }

                //once we have all the possible information gains of this attribute, we need to select the best
                double bestInformationGain = double.MinValue;
                int bestInformationGainIndex = 0;
                for (int j = 0; j < possibleNodes.Count; j++)
                {
                    if(possibleNodes[j].informationGain > bestInformationGain)
                    {
                        bestInformationGain = possibleNodes[j].informationGain;
                        bestInformationGainIndex = j;
                    }
                }

                //once we know the best information gain, we can set our updated matrix
                updatedMatrix = possibleNewMatrixes[bestInformationGainIndex];

            }

            TupleData = updatedMatrix;
        }

        private bool areAnswersSame(List<List<AttributeNode>> attributeSubset)
        {
            List<string> possibleValues = new List<string>();

            foreach(List<AttributeNode> line in attributeSubset)
            {
                string currentAnswer = line.Last().word;
                if(!possibleValues.Contains(currentAnswer))
                    possibleValues.Add(currentAnswer);
            }


            if(possibleValues.Count == 1)
                return true;
            else
                return false;
        }

       
        public TreeNode BuildTreeRecursively(List<List<AttributeNode>> dataset)
        {
            //create a list of possible nodes to place on tree
            List<TreeNode> possibleNodes = new List<TreeNode>();

            //foreach remaining attribute, calculate information gain and add it to the list
            int attributesRemaining = dataset[0].Count() - 1;
            for(int i = 0; i < attributesRemaining; i++)
            {
                possibleNodes.Add(calculateInformationGain(dataset, i));
            }

            //select the node with the highest information gain
            TreeNode selectedNode = getMaxGain(possibleNodes);

            //select subsets from dataset, excluding the attribute that was just selected
            string excludeAttribute = selectedNode.attribute;

            //list that removes the current attribute (column) from table
            List<List<AttributeNode>> updatedData = new List<List<AttributeNode>>();

            //list that contains the possible values for each attribute
            int numberOfValuesInAttribute = 0;
            List<string> valuesPerAttribute = new List<string>();


            //create list of possible values
            foreach (List<AttributeNode> line in dataset)
            {
                for (int i = 0; i < line.Count; i++)
                {
                    if (line[i].classLabel == excludeAttribute)
                    {
                        //check to see if the value for this attribute has been added to the list
                        //this list is used to know how many children to split
                        if (!valuesPerAttribute.Contains(line[i].word))
                        {
                            numberOfValuesInAttribute++;
                            valuesPerAttribute.Add(line[i].word);
                        }

                        //modify the table by removing the attribute from the list, as we do not want to calculate information gain for this attribute anymore
                        line[i].split = true;
                    }
                }

                //add the modified line back to the new table
                updatedData.Add(line);
            }
            

            //build subsets to be used for finding children
            List<List<List<AttributeNode>>> subsets = new List<List<List<AttributeNode>>>();
            List<string> subsetValues = new List<string>();

            //once the table has been modified, select rows based on possible classes for the attribute
            foreach (string possibleValue in valuesPerAttribute)
            {
                List<List<AttributeNode>> valueSubset = new List<List<AttributeNode>>();
                List<List<AttributeNode>> answerSubset = new List<List<AttributeNode>>();
                foreach (List<AttributeNode> line in updatedData)
                {
                    for (int j = 0; j < line.Count; j++)
                    {
                        if (line[j].classLabel == excludeAttribute && line[j].word == possibleValue)
                        {
                            //create a duplicate line, but remove the age attribute
                            List<AttributeNode> modifiedLine = line;
                            modifiedLine.RemoveAt(j);

                            //then add the new line to the subset table, for finding information gain on future attributes
                            valueSubset.Add(modifiedLine);

                            //also add the original line to see if all the values are the same
                            answerSubset.Add(line);
                        }
                    }
                }

                //before adding the subset we just made to the list of subsets for informatio gain processing,
                //check to see if the subset all has the same answer
                if (areAnswersSame(answerSubset))
                {
                    TreeNode child = new TreeNode(excludeAttribute, answerSubset[0].Last().word, possibleValue);
                    selectedNode.AddChild(child);
                }
                else
                {
                    selectedNode.attributeValue = "needs more";
                    subsets.Add(valueSubset);
                    subsetValues.Add(possibleValue);
                }

            }

            //once subset has been created, check to see if all the attributes in the subset result in the same answer
            //if every row in the subset results in the same answer, then you are done
            //otherwise, find information gain for each of the remaining attribute through recursion
            for (int i = 0; i < subsets.Count; i++)
            {
                //otherwise create a child and add it to the list
                TreeNode child = BuildTreeRecursively(subsets[i]);
                child.attribute = excludeAttribute;
                child.attributeValue = subsetValues[i];

                //once the children have been created, add them to the children list
                selectedNode.AddChild(child);
            }


            
            

            //return the new node
            return selectedNode;

        }

        public void StartTree()
        {
            //root = RecursiveBuildTree(TupleData);
            root = BuildTreeRecursively(TupleData);
        }



        private string Print(TreeNode node, int level)
        {
            string display = "";



            if (node.attributeValue != "none" && node.attributeValue != "needs more")
            {
                display += node.attribute + "=" + node.attributeValue + ":";
            }

            if (node.Children.Count == 0)
            {
                display += "\n\t";
                for (int i = 1; i < level; i++)
                    display += "\t";
                display += "ans:" + node.finalAnswer;
            }
            else
            {
                level++;
                foreach (TreeNode child in node.Children)
                {
                    //display +=  "\n" + node.attribute;

                    display += "\n";
                    for (int i = 1; i < level; i++)
                        display += "\t";
                    display += Print(child, level);
                }
            }

            return display;
        }

        public string PrintTree() { return Print(root, 0); }


        //method to view any node and its children on the tree
        private string ViewTree(TreeNode current, int level)
        {
            //starting with the root, view all nodes on the tree using a recursion
            string display = current.View();


            if (current.Children.Count > 0)
            {
                for (int i = 0; i < current.Children.Count; i++)
                {
                    TreeNode child = current.Children[i];
                    for (int j = 0; j < level; j++)
                    {
                        display += "\t";
                    }
                    level++;



                    display += ViewTree(child, level);
                }
            }

            return display;
        }

        //method to view all nodes on the tree
        public string ViewAll() { return ViewTree(root, 0); }



        private TreeNode getMaxGain(List<TreeNode> treeNodes)
        {
            TreeNode maxGain = new TreeNode();
            foreach (TreeNode node in treeNodes)
            {
                if (node.informationGain >= maxGain.informationGain)
                    maxGain = node;
            }
            

            

            return maxGain;
        }


        private TreeNode calculateInformationGain(List<List<AttributeNode>> dataset, int attribute)
        {
            //get the data type of this specifc attribute
            char dataType = dataset.ElementAt(0).ElementAt(attribute).dataType;

            //initialize double
            double infoGain;

            //create counters for however many times each class is found
            List<Data> freqData = new List<Data>();
            double total = 0;
            int numberOfClassesWithinAttribute = 0;

            //Get frequency of each word
            for (int i = 0; i < dataset.Count; i++)
            {
                //Get the attribute contents at current position and at the answer
                AttributeNode currentNode = dataset.ElementAt(i).ElementAt(attribute);
                AttributeNode answerNode = dataset.ElementAt(i).Last();

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

            if (dataType == 'S' && freqData.Count > 0)
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
            for (int a = 0; a < dataset.Count; a++)
            {
                //Get the attribute contents at the answer
                AttributeNode answerNode = dataset.ElementAt(a).Last();
                if (!possibleAnswers.Contains(answerNode.word))
                {
                    possibleAnswers.Add(answerNode.word);
                    frequencies.Add(0);
                }
            }

            for (int a = 0; a < dataset.Count; a++)
            {
                AttributeNode answerNode = dataset.ElementAt(a).Last();
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
            finalOutput += "Expected information for " + possibilities[0] + " is " + Math.Round(finalExpectedInformation, 3) + "\n";
            finalOutput += "Needed information for " + possibilities[0] + " is " + Math.Round(finalInformationNeeded, 3) + "\n";
            finalOutput += "Information gain for " + possibilities[0] + " is " + Math.Round(finalInformationGain, 3) + "\n\n\n";

            return new TreeNode(finalExpectedInformation, finalInformationNeeded, finalInformationGain, possibilities[0], possibilities);
        }





        /********************Information Expected Calculations**********************************/
        private double getInformationExpected(List<double> expectedInformation)
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
        private double getInformationNeeded(List<string> attributes, List<string> possibilities, List<double> probabilties, List<List<double>> splitProbs)
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
            int c = 0;
            foreach (List<double> probs in splitProbs)
            {
                double currentInformationNeeded = expected[c] * getInformationExpected(probs);
                finalInformationNeeded += currentInformationNeeded;
                c++;
            }


            return finalInformationNeeded;
        }
    

        private int numberOfClasses { get; set; }

        private TreeNode root { get; set; }

        private List<List<AttributeNode>> TupleData = new List<List<AttributeNode>>();

        private List<string> AttributeList { get; set; }

    }
}
