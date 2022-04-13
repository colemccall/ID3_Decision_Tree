using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ML_DecisionTreeClassifier
{
    /// <summary>
    /// Interaction logic for DataInterface.xaml
    /// </summary>
    public partial class DataInterface : Window
    {
        private string semesterProjectDirectory { get; set; }
        private string output { get; set; }

        public DataInterface()
        {
            InitializeComponent();
            semesterProjectDirectory = "G:/Shared drives/Machine Learning FireMAP Semester Project/ArcGIS";
        }


        private void RunProgram(string filePath)
        {
            char delimiter = ',';

            //create a new reader to read the file in
            FileReader reader = new FileReader(filePath, delimiter);

            //once the file has been read in, it can be run
            reader.runProgram();

            //once the decision tree has been made, the contents should be printed to the respective textboxes
            Parameters.Text = reader.Display;
            OutFile.Text = reader.OutFile;
            //Input.Text = reader.InFile;
            OutputTree.Text = reader.TreeOutput;
            output = reader.TreeOutput.ToString();
        }


        private void WindowsExplorerButton_Click(object sender, RoutedEventArgs e)
        {
            //Process.Start("explorer.exe", @"C:\Users");
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                string filepath = openFileDialog.FileName;
                RunProgram(filepath);
            }
        }

        private void TestOneButton_Click(object sender, RoutedEventArgs e)
        {
            string filepath = semesterProjectDirectory + "/TrainingData/SVM/Test_1/mesa_training_1_with_annotations.csv";
            RunProgram(filepath);
        }

        private void TestTwoButton_Click(object sender, RoutedEventArgs e)
        {
            string filepath = semesterProjectDirectory + "/TrainingData/SVM/Test_2/Test_2_Training_Samples.csv";
            RunProgram(filepath);
        }

        private void TestThreeButton_Click(object sender, RoutedEventArgs e)
        {
            string filepath = semesterProjectDirectory + "/TrainingData/MaskRCNN/Test1/tree_Samples.csv";
            RunProgram(filepath);
        }

        private void PrintButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //get the name of the file from the text box
                string outputFileName = semesterProjectDirectory + "/DecisionTreeOutputs/" + OutputFileName.Text;

                //create a file stream and open a file to start writing
                StreamWriter outputStreamWriter = new StreamWriter(outputFileName);


                //try to write the output from tree to a file
                outputStreamWriter.Write(output);
                MessageBox.Show(OutputFileName.Text + " successful");
                outputStreamWriter.Close();


            }
            catch (Exception ex)
            {
                MessageBox.Show("Failure - file either could not be opened or could not print contents to file");
            }
        }
    }
}
