using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ML_DecisionTreeClassifier
{
    public class FileReader
    {
        //variables using for opening and reading a file
        private char delimiter { get; set; }
        private string filePath { get; set; }

        //variables using for storing the contents that will be printed out
        public string Display { get; set; }
        public string TreeOutput {  get; set; }
        public string InFile { get; set; }
        public string OutFile { get; set; }

        public FileReader(string filePath, char delimiter)
        {
            this.delimiter = delimiter;
            this.filePath = filePath;
            Display = "";
            TreeOutput = "";
            InFile = "";
            OutFile = "";
        }

        public void runProgram()
        {
            try
            {
                //Open file
                StreamReader reader = new StreamReader(filePath);

                MessageBox.Show("File opened");

                //Read in number of classes
                int numberOfClasses;
                if (delimiter == ',')
                {
                    string currentLine = reader.ReadLine();
                    var currentSections = currentLine.Split(new char[] { delimiter }, StringSplitOptions.RemoveEmptyEntries);
                    numberOfClasses = Convert.ToInt32(currentSections[0]);
                }
                else
                    numberOfClasses = Convert.ToInt32(reader.ReadLine());


                Display += "Number of classes: " + numberOfClasses.ToString() + "\n";
                TreeOutput = "";
                OutFile = "";

                //List to keep track of what classes this file contains
                List<string> classes = new List<string>();

                //List to keep trakc of what types of data each class is 
                List<char> dataTypes = new List<char>();

                //List to keep track of what possible attributes there are 
                List<string> attributeList = new List<string>();

                //Parse the next few lines to get classes and possible values
                for (int i = 0; i <= numberOfClasses; i++)
                {
                    //Read entire line
                    string line = reader.ReadLine();

                    //Split it into parts
                    string[] parts = line.Split(new char[] { delimiter }, StringSplitOptions.RemoveEmptyEntries);

                    //First partition is the class name -> called type
                    string type = parts[0];


                    //if the class is the answer, check to see if it has already been added to the list
                    if (type.ToLower() == "ans")
                    {
                        classes.Add(type);
                        dataTypes.Add('S');
                        for (int a = 1; a < parts.Length; a++)
                        {
                            attributeList.Add(parts[a]);
                        }
                    }
                    else
                    {
                        classes.Add(type);

                        //check if the data is continuous
                        if (parts[1] == "continuous")
                        {
                            dataTypes.Add('C');
                        }

                        //otherwise, consider it to be nominal
                        else
                        {
                            dataTypes.Add('S');
                        }
                    }



                }

                //Print what each classes data type is to the display window
                for (int d = 0; d < classes.Count; d++)
                {
                    if (dataTypes[d] == 'C')
                        Display += classes[d] + " is continuous\n";
                    else if (dataTypes[d] == 'S')
                        Display += classes[d] + " is nominal\n";
                }

                //Create a list that contains all the lines of data
                List<List<AttributeNode>> tuples = new List<List<AttributeNode>>();


                //Read in remaining values
                //Display += "\nTuples\n";
                while (!reader.EndOfStream)
                {
                    //Read the line and split into partitions based on spaces
                    string line = reader.ReadLine();
                    string[] parts = line.Split(new char[] { delimiter }, StringSplitOptions.RemoveEmptyEntries);


                    //Create a list that contains all the nodes on one line
                    List<AttributeNode> currentLine = new List<AttributeNode>();



                    for (int i = 0; i < parts.Length; i++)
                    {
                        //get current class and data type
                        string currentClass = classes[i];
                        char currentType = dataTypes[i];


                        if (currentType == 'C')
                        {
                            double currentValue = double.Parse(parts[i]);
                            //Display.Text += currentValue + " ";
                            AttributeNode node = new AttributeNode(currentClass, currentType, currentValue);
                            currentLine.Add(node);
                        }

                        if (currentType == 'S')
                        {
                            string currentValue = parts[i];
                            //Display.Text += currentValue + " ";
                            AttributeNode node = new AttributeNode(currentClass, currentType, currentValue);
                            currentLine.Add(node);
                        }
                    }


                    ////Test to see if line is being read in correctly
                    //foreach (AttributeNode node in currentLine)
                    //{
                    //    if (node.dataType == 'C')
                    //        Display += node.continuous + " ";
                    //    if (node.dataType == 'S')
                    //        Display += node.word + " ";
                    //}


                    //Add the data from each line to a list of tuples
                    tuples.Add(currentLine);
                    //Display += "\n";
                }


                //display possible answers
                int possibleNumberOfAnswers = 0;
                Display += "\nAnswers\n";
                foreach (string answerPossibility in attributeList)
                {
                    Display += answerPossibility + " ";
                }
                Display += "\n";


                //close file
                reader.Close();


                //quick read for easy comparision
                SpeedRead(filePath);
                string outfile = filePath.Substring(0, filePath.Length - 3) + ".out";
                try { SpeedOut(outfile); }
                catch (Exception ex) { OutFile = "no .out file for this dataset"; }



                //before building the decision tree, call a function that will calculate a split point for each continuous class of data
                //then convert the continous data to a string based on the split point

                //check the first line in the data matrix to see how many continuous attributes there are
                List<int> continuousAttributeIndexes = new List<int>();
                for (int i = 0; i < tuples[0].Count - 1; i++)
                {
                    if (tuples[0].ElementAt(i).dataType == 'C')
                    {
                        continuousAttributeIndexes.Add(i);
                    }
                }

                //now that we know how many contininous attributes there are and the indexes, we can build a list possible split points
                List<List<double>> allPossibleSplitPoints = new List<List<double>>();
                foreach (int continuousIndex in continuousAttributeIndexes)
                {
                    List<double> possibleSplitPoints = new List<double>();
                    foreach (List<AttributeNode> tuple in tuples)
                    {
                        possibleSplitPoints.Add(tuple[0].continuous);
                    }

                    allPossibleSplitPoints.Add(possibleSplitPoints);
                }


                //build decision tree
                Tree DecisionTree = new Tree(tuples, attributeList, numberOfClasses);
                DecisionTree.removeAllContinuous(continuousAttributeIndexes, allPossibleSplitPoints);
                DecisionTree.StartTree();
                TreeOutput = DecisionTree.PrintTree();

            }
            catch (Exception ex)
            {
                Display = ex.Message;
            }
        }



        //methods useful for reading everything in only 2 lines
        private void SpeedRead(string filepath)
        {
            StreamReader stream = new StreamReader(filepath);
            var content = stream.ReadToEnd();
            InFile = content;
            stream.Close();
        }

        private void SpeedOut(string filepath)
        {
            StreamReader stream = new StreamReader(filepath);
            var content = stream.ReadToEnd();
            OutFile = content;
            stream.Close();
        }

    }

    
}
