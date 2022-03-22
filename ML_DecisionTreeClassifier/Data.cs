using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML_DecisionTreeClassifier
{
    public class Data    
    {
        public Data(string word)
        {
            this.word = word;
            count++;
        }

        public Data(double value)
        {
            this.continous = value;
            count++;
        }

        public Data(int value)
        {
            this.integer = value;
            count++;
        }

        //counter to be used to check frequency
        public int count = 0;

        //character to hold data type;
        public char dataType;

        //values to hold data
        public int integer;
        public double continous;
        public string word;
    }
}
