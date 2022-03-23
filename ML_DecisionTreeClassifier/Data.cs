using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML_DecisionTreeClassifier
{
    public class Data    
    {
        public Data(string word, string attributeType)
        {
            this.word = word;
            this.attributeType = attributeType;

            count++;
        }

        public Data(double value, string attributeType)
        {
            this.continous = value;
            this.attributeType = attributeType;

            count++;
        }

        public Data(int value, string attributeType)
        {
            this.integer = value;
            this.attributeType = attributeType;

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
        public string attributeType;


        //test method to see if word exists in struct
        public bool doesContain(string word)
        {
            if (this.word == word)
                return true;
            else
            {
                return false;
            }
        }

        public bool doesContain(int value)
        {
            if (this.integer == value)
                return true;
            else
            {
                return false;
            }
        }
    }
}
