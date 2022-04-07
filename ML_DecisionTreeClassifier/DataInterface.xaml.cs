using Microsoft.Win32;
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
using System.Windows.Shapes;

namespace ML_DecisionTreeClassifier
{
    /// <summary>
    /// Interaction logic for DataInterface.xaml
    /// </summary>
    public partial class DataInterface : Window
    {
        private string semesterProjectDirectory { get; set; }   

        public DataInterface()
        {
            InitializeComponent();
            semesterProjectDirectory = "G:/Shared drives/Machine Learning FireMAP Semester Project/ArcGIS/TrainingData";
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
            string filepath = semesterProjectDirectory + "/SVM/Test_1/mesa_training_1_with_annotations.csv";
            RunProgram(filepath);
        }
    }
}
