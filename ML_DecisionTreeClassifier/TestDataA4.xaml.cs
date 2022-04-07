using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using Microsoft.Win32;
using System.Diagnostics;
using System.Numerics;

namespace ML_DecisionTreeClassifier
{
    public partial class TestDataA4 : Window
    {
        private string filedir = Directory.GetCurrentDirectory() + "\\.." + "\\.." + "\\..";
        private string decisionTreeOutput;
        private char delimiter { get; set; }    

        public TestDataA4()
        {
            InitializeComponent();
        }



        private void RunProgram(string filePath)
        {
            string fileType = FileTypeBox.Text;

            if (fileType == ".csv" || fileType == "csv")
                delimiter = ',';
            else
                delimiter = ' ';

            //create a new reader to read the file in
            FileReader reader = new FileReader(filePath, delimiter);

            //once the file has been read in, it can be run
            reader.runProgram();

            //once the decision tree has been made, the contents should be printed to the respective textboxes
            Display.Text = reader.Display;
            OutfileBox.Text = reader.OutFile;
            FileInput.Text = reader.InFile;
            Test.Text = reader.TreeOutput;
        }


        private void RunA_Click(object sender, RoutedEventArgs e)
        {
            string filepath = filedir + "\\testDataA4\\a.in";
            RunProgram(filepath);
        }

        private void StressTest_Click(object sender, RoutedEventArgs e)
        {
            string filepath = filedir + "\\semesterProjectData\\diabetes_dataset.txt";
            FileTypeBox.Text = "txt";
            delimiter = ' ';
            RunProgram(filepath);
        }

        private void TestOneSamples_Click(object sender, RoutedEventArgs e)
        {
            string filepath = "G:/Shared drives/Machine Learning FireMAP Semester Project/ArcGIS/TrainingData/SVM/Test_1/mesa_training_1_with_annotations.csv";
            FileTypeBox.Text = "csv";
            delimiter = ',';
            RunProgram(filepath);
        }

        private void CircuitButton_Click(object sender, RoutedEventArgs e)
        {
            string filepath = filedir + "\\testDataA4\\circuit.in";
            RunProgram(filepath);
        }

        private void ContinueButton_Click(object sender, RoutedEventArgs e)
        {
            string filepath = filedir + "\\testDataA4\\continue.in";
            RunProgram(filepath);
        }

        private void Continue0Button_Click(object sender, RoutedEventArgs e)
        {
            string filepath = filedir + "\\testDataA4\\continue0.in";
            RunProgram(filepath);
        }

        private void Continue2Button_Click(object sender, RoutedEventArgs e)
        {
            string filepath = filedir + "\\testDataA4\\continue2.in";
            RunProgram(filepath);
        }

        private void GolfButton_Click(object sender, RoutedEventArgs e)
        {
            string filepath = filedir + "\\testDataA4\\golf.in";
            RunProgram(filepath);
        }

        private void GolfcButton_Click(object sender, RoutedEventArgs e)
        {
            string filepath = filedir + "\\testDataA4\\golfc.in";
            RunProgram(filepath);
        }

        private void NotaButton_Click(object sender, RoutedEventArgs e)
        {
            string filepath = filedir + "\\testDataA4\\nota.in";
            RunProgram(filepath);
        }

        private void OrButton_Click(object sender, RoutedEventArgs e)
        {
            string filepath = filedir + "\\testDataA4\\or.in";
            RunProgram(filepath);
        }

        private void Parity3Button_Click(object sender, RoutedEventArgs e)
        {
            string filepath = filedir + "\\testDataA4\\parity3.in";
            RunProgram(filepath);
        }

        private void RestaurantButton_Click(object sender, RoutedEventArgs e)
        {
            string filepath = filedir + "\\testDataA4\\restaurantDecisionTree.in";
            RunProgram(filepath);
        }

        private void SimpleButton_Click(object sender, RoutedEventArgs e)
        {
            string filepath = filedir + "\\testDataA4\\simple.in";
            RunProgram(filepath);
        }

        private void SomeParity3_Click(object sender, RoutedEventArgs e)
        {
            string filepath =filedir + "\\testDataA4\\someparity3.in";
            RunProgram(filepath);
        }

        private void Split_Click(object sender, RoutedEventArgs e)
        {
            string filepath = filedir + "\\testDataA4\\split.in";
            RunProgram(filepath);
        }

        private void TooLittleButton_Click(object sender, RoutedEventArgs e)
        {
            string filepath = filedir + "\\testDataA4\\toolittle.in";
            RunProgram(filepath);
        }

        private void XorButton_Click(object sender, RoutedEventArgs e)
        {
            string filepath = filedir + "\\testDataA4\\xor.in";
            RunProgram(filepath);
        }

        private void XorcButton_Click(object sender, RoutedEventArgs e)
        {
            string filepath = filedir + "\\testDataA4\\xorc.in";
            RunProgram(filepath);
        }

        private void testTree_Click(object sender, RoutedEventArgs e)
        {
            string filepath = filedir + "\\testDataA4\\testTree.in";
            RunProgram(filepath);
        }

        private void SelectFile_Click(object sender, RoutedEventArgs e)
        {
            //Process.Start("explorer.exe", @"C:\Users");
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                string filepath = openFileDialog.FileName;
                FileSelected.Text = filepath;
                RunProgram(filepath);
            }


            // FileSelected.Text = openFileDialog1.FileName;

        }

        private void FileSelected_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void PrintButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //get the name of the file from the text box
                string outputFileName = filedir + "\\outputs\\" + OutputFileName.Text;

                //create a file stream and open a file to start writing
                StreamWriter outputStreamWriter = new StreamWriter(outputFileName);


                //try to write the output from tree to a file
                outputStreamWriter.Write(decisionTreeOutput);
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
