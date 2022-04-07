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
    /// Interaction logic for SelectType.xaml
    /// </summary>
    public partial class MainMenu : Window
    {
        
        public MainMenu()
        {
            InitializeComponent();
           
        }

        private void Program1Button_Click(object sender, RoutedEventArgs e)
        {
            //open the window made for running testDataA4
            TestDataA4 window = new TestDataA4();
            window.Show();
        }

        private void Program2Button_Click(object sender, RoutedEventArgs e)
        {
            DataInterface window = new DataInterface();
            window.Show();
        }
    }
}
