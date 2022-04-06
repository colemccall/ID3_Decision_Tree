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
    public partial class SelectType : Window
    {
        public char dataType { get; set; }
        public bool buttonPressed { get; set; }
        public SelectType()
        {
            InitializeComponent();
            dataType = 'a';
            buttonPressed = false;
        }

        private void CommaButton_Click(object sender, RoutedEventArgs e)
        {
            dataType = ',';
            buttonPressed = true;
        }

        private void TextButton_Click(object sender, RoutedEventArgs e)
        {
            dataType = ' ';
            buttonPressed = true;
        }
    }
}
